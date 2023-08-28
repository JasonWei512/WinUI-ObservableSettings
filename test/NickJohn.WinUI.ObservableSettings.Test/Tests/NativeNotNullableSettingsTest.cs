using NickJohn.WinUI.ObservableSettings.Test.Testers;

namespace NickJohn.WinUI.ObservableSettings.Test.Tests;

public partial class NativeNotNullableSettings
{
    [ObservableSetting] private readonly System.Int16 int16 = default;
    [ObservableSetting] private readonly System.UInt16 uInt16 = default;
    [ObservableSetting] private readonly System.Int32 int32 = default;
    [ObservableSetting] private readonly System.UInt32 uInt32 = default;
    [ObservableSetting] private readonly System.Int64 int64 = default;
    [ObservableSetting] private readonly System.UInt64 uInt64 = default;
    [ObservableSetting] private readonly System.Single single = default;
    [ObservableSetting] private readonly System.Double @double = default;
    [ObservableSetting] private readonly System.Boolean boolean = default;
    [ObservableSetting] private readonly System.Char @char = default;
    [ObservableSetting] private readonly System.DateTimeOffset dateTimeOffset = default;
    [ObservableSetting] private readonly System.TimeSpan timeSpan = default;
    [ObservableSetting] private readonly System.Guid guid = default;
    [ObservableSetting] private readonly Windows.Foundation.Point point = default;
    [ObservableSetting] private readonly Windows.Foundation.Size size = default;
    [ObservableSetting] private readonly Windows.Foundation.Rect rect = default;
    [ObservableSetting] private readonly System.String @string = string.Empty;
}

[TestClass]
public class NativeNotNullableSettingsTest : SettingsTest<NativeNotNullableSettings>
{
    [DataTestMethod]
    [DynamicData(nameof(Data_NativeNotNullableSettings_AssignMultipleTimes))]
    public void NativeNotNullableSettings_AssignMultipleTimes(SettingTester<NativeNotNullableSettings> settingTester)
    {
        NativeNotNullableSettings settings = new();
        settingTester.Test(settings);
    }

    public static DynamicDataSource<SettingTester<NativeNotNullableSettings>> Data_NativeNotNullableSettings_AssignMultipleTimes => new()
    {
        NativeSettingTester<System.Int16>(s => s.Int16, 1, 2, 3),
        NativeSettingTester<System.UInt16>(s => s.UInt16, 1, 2, 3),
        NativeSettingTester<System.Int32>(s => s.Int32, 1, 2, 3),
        NativeSettingTester<System.UInt32>(s => s.UInt32, 1, 2, 3),
        NativeSettingTester<System.Int64>(s => s.Int64, 1, 2, 3),
        NativeSettingTester<System.UInt64>(s => s.UInt64, 1, 2, 3),
        NativeSettingTester<System.Single>(s => s.Single, 1.5f, -2.5f, 3.5f),
        NativeSettingTester<System.Double>(s => s.Double, 1.5, -2.5, 3.5),
        NativeSettingTester<System.Boolean>(s => s.Boolean, true, false, true),
        NativeSettingTester<System.Char>(s => s.Char, 'A', 'b', 'C'),

        NativeSettingTester<System.DateTimeOffset>(s => s.DateTimeOffset,
            new DateTimeOffset(new DateTime(1926, 08, 17, 22, 11, 30)),
            new DateTimeOffset(new DateTime(1926, 08, 17, 00, 00, 00)),
            new DateTimeOffset(new DateTime(1926, 08, 17, 18, 18, 00))),

        NativeSettingTester<System.TimeSpan>(s => s.TimeSpan,
            new TimeSpan(11, 45, 14),
            new TimeSpan(12, 34, 56),
            new TimeSpan(00, 00, 00)),

        NativeSettingTester<System.Guid>(s => s.Guid,
            new Guid("b7a09284-8104-4533-be18-3570f082e554"),
            new Guid("802b6015-66f2-4229-af9d-4f1d9fe698a6"),
            new Guid("4794b50f-9210-4268-85eb-d7edebb798f1")),

        NativeSettingTester<Windows.Foundation.Point>(s => s.Point,
            new Windows.Foundation.Point(1, 2),
            new Windows.Foundation.Point(3, 4),
            new Windows.Foundation.Point(5, 6)),

        NativeSettingTester<Windows.Foundation.Size>(s => s.Size,
            new Windows.Foundation.Size(3, 4),
            new Windows.Foundation.Size(5, 6),
            new Windows.Foundation.Size(7, 8)),

        NativeSettingTester<Windows.Foundation.Rect>(s => s.Rect,
            new Windows.Foundation.Rect(5, 6, 7, 8),
            new Windows.Foundation.Rect(1, 2, 3, 4),
            new Windows.Foundation.Rect(5, 6, 7, 8)),

        NativeSettingTester<System.String>(s => s.String, "Hahaha", "哈哈哈", "ははは"),
    };
}