using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NickJohn.WinUI.ObservableSettings.Core.Constants;
using NickJohn.WinUI.ObservableSettings.SourceGenerator.Helpers;
using NickJohn.WinUI.ObservableSettings.SourceGenerator.Models;
using System.Collections.Immutable;
using System.Text;

namespace NickJohn.WinUI.ObservableSettings.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class ObservableSettingGenerator : IIncrementalGenerator
{
    private const string ObservableSettingAttributeFullName = "NickJohn.WinUI.ObservableSettings.ObservableSettingAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        Logger.DeleteLog();

        // Do a simple filter for fields
        IncrementalValuesProvider<FieldDeclarationSyntax> fieldDeclarations =
            context.SyntaxProvider
                .CreateSyntaxProvider(IsSyntaxTargetForGeneration, GetSemanticTargetForGeneration)
                .Where(f => f is not null)!;

        // Combine the selected fields with the `Compilation`
        IncrementalValueProvider<(Compilation, ImmutableArray<FieldDeclarationSyntax>)> compilationAndFields =
            context.CompilationProvider.Combine(fieldDeclarations.Collect());

        // Generate the source using the compilation and enums
        context.RegisterSourceOutput(compilationAndFields,
            (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    private bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return syntaxNode is FieldDeclarationSyntax field && field.AttributeLists.Any();
    }

    private FieldDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext syntaxContext, CancellationToken cancellationToken)
    {
        if (syntaxContext.Node is not FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            return null;
        }

        foreach (AttributeSyntax attributeSyntax in fieldDeclarationSyntax.AttributeLists.SelectMany(a => a.Attributes))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (syntaxContext.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
            {
                // weird, we couldn't get the symbol, ignore it
                continue;
            }

            string attributeFullName = attributeSymbol.ContainingType.ToDisplayString();

            if (attributeFullName == ObservableSettingAttributeFullName)
            {
                return fieldDeclarationSyntax;
            }
        }

        return null;
    }

    private void Execute(Compilation compilation, ImmutableArray<FieldDeclarationSyntax> fields, SourceProductionContext context)
    {
        CancellationToken cancellationToken = context.CancellationToken;
        cancellationToken.ThrowIfCancellationRequested();

        if (compilation.GetTypeByMetadataName(ObservableSettingAttributeFullName) is not INamedTypeSymbol attributeSymbol)
        {
            return;
        }

        if (compilation.GetTypeByMetadataName("System.ComponentModel.INotifyPropertyChanged") is not INamedTypeSymbol notifySymbol)
        {
            return;
        }

        if (fields.IsDefaultOrEmpty)
        {
            return;
        }

        IEnumerable<FieldDeclarationSyntax> distinctFields = fields.Distinct();

        IReadOnlyList<FieldToGenerate> fieldsToGenerate = GetFieldsToGenerate(compilation, distinctFields, cancellationToken);

        if (fieldsToGenerate.Any())
        {
            // group the fields by class, and generate the source
            foreach (IGrouping<INamedTypeSymbol, FieldToGenerate> group in fieldsToGenerate.GroupBy<FieldToGenerate, INamedTypeSymbol>(f => f.ParentClass, SymbolEqualityComparer.Default))
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (GeneratePropertiesClass(group.Key, group.ToList(), attributeSymbol, notifySymbol) is string classSource)
                {
                    string fileName = $"{group.Key.GetFullName()}_ObservableSettings.g.cs";
                    Logger.Log($$"""
                        Generating {{fileName}}:
                        {{classSource}}

                        """);
                    context.AddSource(fileName, classSource);
                }
            }
        }
    }

    private IReadOnlyList<FieldToGenerate> GetFieldsToGenerate(Compilation compilation, IEnumerable<FieldDeclarationSyntax> distinctFields, CancellationToken cancellationToken)
    {
        List<FieldToGenerate> results = new();

        foreach (FieldDeclarationSyntax fieldDeclarationSyntax in distinctFields)
        {
            cancellationToken.ThrowIfCancellationRequested();
            results.AddRange(FieldToGenerate.GetFrom(compilation, fieldDeclarationSyntax));
        }

        return results;
    }

    private string? GeneratePropertiesClass(INamedTypeSymbol classSymbol, IEnumerable<FieldToGenerate> fieldsToGenerate, INamedTypeSymbol attributeSymbol, INamedTypeSymbol notifySymbol)
    {
        if (!classSymbol.ContainingSymbol.Equals(classSymbol.ContainingNamespace, SymbolEqualityComparer.Default))
        {
            return null; //TODO: issue a diagnostic that it must be top level
        }

        string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

        // begin building the generated source
        StringBuilder source = new();

        source.AppendLine($$"""
            #nullable enable

            namespace {{namespaceName}}
            {
                public partial class {{classSymbol.Name}} : global::{{notifySymbol.ToDisplayString()}}
                {
            """);

        // if the class doesn't implement INotifyPropertyChanged already, add it
        source.AppendLineIf(!classSymbol.Interfaces.Contains(notifySymbol, SymbolEqualityComparer.Default), """
                    public event global::System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

            """);

        foreach (FieldToGenerate fieldToGenerate in fieldsToGenerate)
        {
            ProcessProperty(source, fieldToGenerate, attributeSymbol);
        }

        source.AppendLine("""
                }
            }
            """);

        return source.ToString();
    }

    private void ProcessProperty(StringBuilder source, FieldToGenerate fieldToGenerate, ISymbol attributeSymbol)
    {
        IFieldSymbol fieldSymbol = fieldToGenerate.FieldSymbol;

        // get the name and type of the field
        string fieldName = fieldSymbol.Name;
        ITypeSymbol fieldType = fieldSymbol.Type;
        string fieldTypeNameWithGlobalPrefix = fieldType.GetFullNameWithGlobalPrefix();
        bool isNullableType = fieldType.IsNullableType();
        bool isNativeSettingType = SettingTypes.NativeSettingTypes.Contains(fieldType.GetFullName().TrimEnd('?'));

        // get the AutoNotify attribute from the field, and any associated data
        AttributeData? attributeData = fieldSymbol.GetAttributes().SingleOrDefault(ad => ad.AttributeClass switch
        {
            INamedTypeSymbol attributeClass => attributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default),
            _ => false
        });

        string? overridenSettingKey = attributeData?.ConstructorArguments.ElementAtOrDefault(0).Value as string;
        bool raiseEvent = attributeData?.ConstructorArguments.ElementAtOrDefault(1).Value as bool? ?? true;

        string propertyName = ChoosePropertyNameForField(fieldName);
        string settingKey = overridenSettingKey ?? propertyName;

        source.AppendLineIf(raiseEvent, $$"""
                    /// <summary>
                    /// Fired when the value of <see cref="{{propertyName}}"/> changes.
                    /// </summary>
                    public event global::System.EventHandler<global::NickJohn.WinUI.ObservableSettings.SettingValueChangedEventArgs<{{fieldTypeNameWithGlobalPrefix}}>>? {{propertyName}}Changed;
                    
            """);

        source.AppendLineIf(!string.IsNullOrEmpty(fieldToGenerate.CommentsAndDocuments), fieldToGenerate.CommentsAndDocuments?.Indent(8));

        source.AppendLine($$"""
                    public {{fieldTypeNameWithGlobalPrefix}} @{{propertyName}}
                    {
            """);

        source.AppendLine($$"""
                        get
                        {
                            return global::NickJohn.WinUI.ObservableSettings.Internal.SettingsManager.{{(isNativeSettingType ? $"GetNativeSetting" : $"GetJsonSetting")}}{{(isNullableType ? "Nullable" : "")}}("{{settingKey}}", @{{fieldName}});
                        }
                        set
                        {
                            {{fieldTypeNameWithGlobalPrefix}} oldValue = {{propertyName}};
                            if (!global::System.Collections.Generic.EqualityComparer<{{fieldTypeNameWithGlobalPrefix}}>.Default.Equals(oldValue, value))
                            {
                                global::NickJohn.WinUI.ObservableSettings.Internal.SettingsManager.{{(isNativeSettingType ? "SetNativeSetting" : "SetJsonSetting")}}("{{settingKey}}", value);
                                PropertyChanged?.Invoke(this, new global::System.ComponentModel.PropertyChangedEventArgs("{{propertyName}}"));
            """);

        source.AppendLineIf(raiseEvent, $$"""
                                {{propertyName}}Changed?.Invoke(this, new global::NickJohn.WinUI.ObservableSettings.SettingValueChangedEventArgs<{{fieldTypeNameWithGlobalPrefix}}>(oldValue, value));
            """);

        source.AppendLine("""
                            }
                        }
                    }

            """);
    }

    private string ChoosePropertyNameForField(string fieldName)
    {
        fieldName = fieldName.TrimStart('_');

        if (fieldName.Length == 0)
        {
            return string.Empty;
        }

        if (fieldName.Length == 1)
        {
            return fieldName.ToUpper();
        }

        return fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
    }
}
