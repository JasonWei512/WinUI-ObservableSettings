using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NickJohn.WinUI.ObservableSettings.SourceGenerator.Models;

public class FieldToGenerate
{
    public IFieldSymbol FieldSymbol { get; set; }
    public INamedTypeSymbol ParentClass { get; set; }

    public string? CommentsAndDocuments { get; set; }

    public FieldToGenerate(IFieldSymbol fieldSymbol)
    {
        FieldSymbol = fieldSymbol;
        ParentClass = fieldSymbol.ContainingType;
    }

    public static IReadOnlyList<FieldToGenerate> GetFrom(Compilation compilation, FieldDeclarationSyntax fieldDeclarationSyntax)
    {
        List<FieldToGenerate> results = new();

        string? commentsAndDocuments = GetCommentsAndDocuments(fieldDeclarationSyntax);

        foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
        {
            SemanticModel semanticModel = compilation.GetSemanticModel(variable.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(variable) is IFieldSymbol fieldSymbol)
            {
                FieldToGenerate fieldToGenerate = new(fieldSymbol)
                {
                    CommentsAndDocuments = commentsAndDocuments
                };
                results.Add(fieldToGenerate);
                commentsAndDocuments = null;
            }
        }

        return results;
    }

    private static readonly SyntaxKind[] commentsAndDocumentsKinds = new[]
    {
         SyntaxKind.SingleLineCommentTrivia,
         SyntaxKind.MultiLineCommentTrivia,
         SyntaxKind.SingleLineDocumentationCommentTrivia,
         SyntaxKind.MultiLineDocumentationCommentTrivia,
     };

    private static string? GetCommentsAndDocuments(FieldDeclarationSyntax fieldDeclarationSyntax)
    {
        IEnumerable<string> commentsAndDocumentsList = fieldDeclarationSyntax.DescendantTrivia()
               .Where(t => commentsAndDocumentsKinds.Any(kind => t.IsKind(kind)))
               .Select(t => t.ToFullString())
               .Where(s => !string.IsNullOrWhiteSpace(s));

        string commentsAndDocuments = string.Join("\r\n", commentsAndDocumentsList);

        return string.IsNullOrWhiteSpace(commentsAndDocuments) ? null : commentsAndDocuments;
    }
}