using System.Text;

namespace NickJohn.WinUI.ObservableSettings.SourceGenerator.Helpers;

public static class StringHelper
{
    /// <summary>
    /// Add <paramref name="spaces"/> to each line of the string <paramref name="value"/>.
    /// </summary>
    public static string Indent(this string value, string spaces)
    {
        StringBuilder stringBuilder = new();
        using StringReader stringReader = new(value);

        while (stringReader.ReadLine() is string line)
        {
            stringBuilder.Append(spaces);
            stringBuilder.AppendLine(line);
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Add spaces of <paramref name="spaceCount"/> to each line of the string <paramref name="value"/>.
    /// </summary>
    public static string Indent(this string value, int spaceCount) => Indent(value, new string(' ', spaceCount));

    /// <summary>
    /// Append <paramref name="value"/> to <paramref name="stringBuilder"/> if <paramref name="predicate"/> is true.
    /// </summary>
    public static StringBuilder AppendLineIf(this StringBuilder stringBuilder, bool predicate, string? value)
    {
        if (value is null) { return stringBuilder; }

        if (predicate)
        {
            stringBuilder.AppendLine(value);
        }
        return stringBuilder;
    }
}
