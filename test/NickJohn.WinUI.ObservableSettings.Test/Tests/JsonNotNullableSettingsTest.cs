using NickJohn.WinUI.ObservableSettings.Test.Testers;

namespace NickJohn.WinUI.ObservableSettings.Test.Tests;

public partial class JsonNotNullableSettings
{
    [ObservableSetting] private readonly List<int> intList = new();
    [ObservableSetting] private readonly int[] intArray = new int[0];
    [ObservableSetting] private readonly List<string> stringList = new();
    [ObservableSetting] private readonly string[] stringArray = new string[0];
    [ObservableSetting] private readonly Version version = new();
}

[TestClass]
public class JsonNotNullableSettingsTest : SettingsTest<JsonNotNullableSettings>
{
    [DataTestMethod]
    [DynamicData(nameof(Data_JsonNotNullableSettings_AssignMultipleTimes))]
    public void JsonNotNullableSettings_AssignMultipleTimes(SettingTester<JsonNotNullableSettings> settingTester)
    {
        JsonNotNullableSettings settings = new();
        settingTester.Test(settings);
    }

    public static DynamicDataSource<SettingTester<JsonNotNullableSettings>> Data_JsonNotNullableSettings_AssignMultipleTimes => new()
    {
        JsonSettingTester<List<int>>(
            s => s.IntList,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 4, 5, 6 },
                new List<int>() { 7, 8, 9 },
            }),

        JsonSettingTester<int[]>(
            s => s.IntArray,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 },
            }),

        JsonSettingTester<List<string>>(
            s => s.StringList,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new List<string>() { "Hahaha", "哈哈哈", "ははは" },
                new List<string>() { "하하하", "хахаха", "www" },
                new List<string>() { "jajaja", "233", "草" },
            }),

        JsonSettingTester<string[]>(
            s => s.StringArray,
            (a, b) => a.SequenceEqual(b),
            new[]
            {
                new[] { "Hahaha", "哈哈哈", "ははは" },
                new[] { "하하하", "хахаха", "www" },
                new[] { "jajaja", "233", "草" },
            }),

        JsonSettingTester<Version>(
            s => s.Version,
            (a,b) => a.Equals(b),
            new[]
            {
                new Version(1, 0, 0),
                new Version(2, 3, 3),
                new Version(11, 45, 14),
            })
    };
}
