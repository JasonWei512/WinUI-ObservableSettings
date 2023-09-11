using Microsoft.CodeAnalysis;

namespace NickJohn.WinUI.ObservableSettings.SourceGenerator.Helpers;

public static class RoslynExtensions
{
    private static readonly SymbolDisplayFormat GlobalPrefixFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers);

    private static readonly SymbolDisplayFormat NoGlobalPrefixFormat = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers);

    public static string GetFullNameWithGlobalPrefix(this ISymbol symbol)
    {
        string fullName = symbol.ToDisplayString(GlobalPrefixFormat);

        if (symbol is ITypeSymbol typeSymbol)
        {
            // If "typeSymbol" is a reference type and annotated with nullable annotation "?", then "fullName" will not have "?" at the end.
            // We have to manually add the "?" to the end.
            if (typeSymbol.IsNullableType() && !fullName.EndsWith("?"))
            {
                fullName += "?";
            }
        }

        return fullName;
    }

    public static string GetFullName(this ISymbol symbol)
    {
        string fullName = symbol.ToDisplayString(NoGlobalPrefixFormat);

        if (symbol is ITypeSymbol typeSymbol)
        {
            // If "typeSymbol" is a reference type and annotated with nullable annotation "?", then "fullName" will not have "?" at the end.
            // We have to manually add the "?" to the end.
            if (typeSymbol.IsNullableType() && !fullName.EndsWith("?"))
            {
                fullName += "?";
            }
        }

        return fullName;
    }

    public static bool IsNullableType(this ITypeSymbol typeSymbol)
    {
        return
            typeSymbol.NullableAnnotation == NullableAnnotation.Annotated ||
            typeSymbol.ToDisplayString(NoGlobalPrefixFormat).EndsWith("?");
    }
}
