using System.Diagnostics;

namespace NickJohn.WinUI.ObservableSettings.SourceGenerator.Helpers;

/// <summary>
/// This will log to <see href="%TEMP%/SourceGeneratorDebug.txt" />. <br/>
/// You can press Win+R, input %TEMP% and enter, and see this file.
/// </summary>
internal static class Logger
{
#pragma warning disable RS1035 // Do not use APIs banned for analyzers
    private static readonly string LogFilePath = Path.Combine(Path.GetTempPath(), "SourceGeneratorDebug.txt");
#pragma warning restore RS1035 // Do not use APIs banned for analyzers

    internal static void DeleteLog()
    {
#if DEBUG
        try
        {
#pragma warning disable RS1035 // Do not use APIs banned for analyzers
            File.Delete(LogFilePath);
#pragma warning restore RS1035 // Do not use APIs banned for analyzers
        }
        catch { }
#endif
    }

    internal static void Log(string? content)
    {
#if DEBUG
        Debug.WriteLine(content);
#pragma warning disable RS1035 // Do not use APIs banned for analyzers
        File.AppendAllLines(LogFilePath, new[] { content ?? "Null" });
#pragma warning restore RS1035 // Do not use APIs banned for analyzers
#endif
    }
}
