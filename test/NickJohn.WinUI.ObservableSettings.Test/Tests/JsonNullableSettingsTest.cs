using NickJohn.WinUI.ObservableSettings.Test.Testers;

namespace NickJohn.WinUI.ObservableSettings.Test.Tests;

public partial class JsonNullableSettings
{
    [ObservableSetting] private readonly List<int>? intList = null;
    [ObservableSetting] private readonly int[]? intArray = null;
    [ObservableSetting] private readonly List<string>? stringList = null;
    [ObservableSetting] private readonly string[]? stringArray = null;
    [ObservableSetting] private readonly Version? version = null;
}

[TestClass]
public class JsonNullableSettingsTest : SettingsTest<JsonNullableSettings>
{
    [DataTestMethod]
    [DynamicData(nameof(Data_JsonNullableSettings_AssignMultipleTimes))]
    public void JsonNullableSettings_AssignMultipleTimes(SettingTester<JsonNullableSettings> settingTester)
    {
        JsonNullableSettings settings = new();
        settingTester.Test(settings);
    }

    public static DynamicDataSource<SettingTester<JsonNullableSettings>> Data_JsonNullableSettings_AssignMultipleTimes => new()
    {
        JsonSettingTester<List<int>>(
            s => s.IntList,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new List<int>() { 1, 2, 3 },
                null,
                new List<int>() { 4, 5, 6 },
                null,
                new List<int>() { 7, 8, 9 },
            }),

        JsonSettingTester<int[]>(
            s => s.IntArray,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new[] { 1, 2, 3 },
                null,
                new[] { 4, 5, 6 },
                null,
                new[] { 7, 8, 9 },
            }),

        JsonSettingTester<List<string>>(
            s => s.StringList,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new List<string>() { "Hahaha", "哈哈哈", "ははは" },
                null,
                new List<string>() { "하하하", "хахаха", "www" },
                null,
                new List<string>() { "jajaja", "233", "草" },
            }),

        JsonSettingTester<string[]>(
            s => s.StringArray,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new[] { "Hahaha", "哈哈哈", "ははは" },
                null,
                new[] { "하하하", "хахаха", "www" },
                null,
                new[] { "jajaja", "233", "草" },
            }),

        JsonSettingTester<Version>(
            s => s.Version,
            (a,b) => a.Equals(b),
            new[]
            {
                new Version(1, 0, 0),
                null,
                new Version(2, 3, 3),
                null,
                new Version(11, 45, 14),
            })
    };
}
