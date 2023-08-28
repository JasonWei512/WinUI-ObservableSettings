using NickJohn.WinUI.ObservableSettings.Test.Testers;

namespace NickJohn.WinUI.ObservableSettings.Test.Tests;

public partial class NativeNullableSettings
{
    [ObservableSetting] private readonly System.Int16? int16Null = null;
    [ObservableSetting] private readonly System.UInt16? uInt16Null = null;
    [ObservableSetting] private readonly System.Int32? int32Null = null;
    [ObservableSetting] private readonly System.UInt32? uInt32Null = null;
    [ObservableSetting] private readonly System.Int64? int64Null = null;
    [ObservableSetting] private readonly System.UInt64? uInt64Null = null;
    [ObservableSetting] private readonly System.Single? singleNull = null;
    [ObservableSetting] private readonly System.Double? doubleNull = null;
    [ObservableSetting] private readonly System.Boolean? booleanNull = null;
    [ObservableSetting] private readonly System.Char? charNull = null;
    [ObservableSetting] private readonly System.DateTimeOffset? dateTimeOffsetNull = null;
    [ObservableSetting] private readonly System.TimeSpan? timeSpanNull = null;
    [ObservableSetting] private readonly System.Guid? guidNull = null;
    [ObservableSetting] private readonly Windows.Foundation.Point? pointNull = null;
    [ObservableSetting] private readonly Windows.Foundation.Size? sizeNull = null;
    [ObservableSetting] private readonly Windows.Foundation.Rect? rectNull = null;
    [ObservableSetting] private readonly System.String? @stringNull = null;
}

[TestClass]
public partial class NativeNullableSettingsTest : SettingsTest<NativeNullableSettings>
{
    [DataTestMethod]
    [DynamicData(nameof(Data_NativeNullableSettings_AssignMultipleTimes))]
    public void NativeNullableSettings_AssignMultipleTimes(SettingTester<NativeNullableSettings> settingTester)
    {
        NativeNullableSettings settings = new();
        settingTester.Test(settings);
    }

    public static DynamicDataSource<SettingTester<NativeNullableSettings>> Data_NativeNullableSettings_AssignMultipleTimes => new()
    {
        NativeSettingTester<System.Int16?>(s => s.Int16Null, 1, null, 2, null, 3),
        NativeSettingTester<System.UInt16?>(s => s.UInt16Null, 1, null, 2, null, 3),
        NativeSettingTester<System.Int32?>(s => s.Int32Null, 1, null, 2, null, 3),
        NativeSettingTester<System.UInt32?>(s => s.UInt32Null, 1, null, 2, null, 3),
        NativeSettingTester<System.Int64?>(s => s.Int64Null, 1, null, 2, null, 3),
        NativeSettingTester<System.UInt64?>(s => s.UInt64Null, 1, null, 2, null, 3),
        NativeSettingTester<System.Single?>(s => s.SingleNull, 1.5f, null, -2.5f, null, 3.5f),
        NativeSettingTester<System.Double?>(s => s.DoubleNull, 1.5, null, -2.5, null, 3.5),
        NativeSettingTester<System.Boolean?>(s => s.BooleanNull, true, null, false, null, true),
        NativeSettingTester<System.Char?>(s => s.CharNull, 'A', null, 'b', null, 'C'),

        NativeSettingTester<System.DateTimeOffset?>(s => s.DateTimeOffsetNull,
            new DateTimeOffset(new DateTime(1926, 08, 17, 22, 11, 30)),
            null,
            new DateTimeOffset(new DateTime(1926, 08, 17, 00, 00, 00)),
            null,
            new DateTimeOffset(new DateTime(1926, 08, 17, 18, 18, 00))),

        NativeSettingTester<System.TimeSpan?>(s => s.TimeSpanNull,
            new TimeSpan(11, 45, 14),
            null,
            new TimeSpan(12, 34, 56),
            null,
            new TimeSpan(00, 00, 00)),

        NativeSettingTester<System.Guid?>(s => s.GuidNull,
            new Guid("b7a09284-8104-4533-be18-3570f082e554"),
            null,
            new Guid("802b6015-66f2-4229-af9d-4f1d9fe698a6"),
            null,
            new Guid("4794b50f-9210-4268-85eb-d7edebb798f1")),

        NativeSettingTester<Windows.Foundation.Point?>(s => s.PointNull,
            new Windows.Foundation.Point(1, 2),
            null,
            new Windows.Foundation.Point(3, 4),
            null,
            new Windows.Foundation.Point(5, 6)),

        NativeSettingTester<Windows.Foundation.Size?>(s => s.SizeNull,
            new Windows.Foundation.Size(3, 4),
            null,
            new Windows.Foundation.Size(5, 6),
            null,
            new Windows.Foundation.Size(7, 8)),

        NativeSettingTester<Windows.Foundation.Rect?>(s => s.RectNull,
            new Windows.Foundation.Rect(5, 6, 7, 8),
            null,
            new Windows.Foundation.Rect(1, 2, 3, 4),
            null,
            new Windows.Foundation.Rect(5, 6, 7, 8)),

        NativeSettingTester<System.String?>(s => s.StringNull, "Hahaha", null, "哈哈哈", null, "ははは"),
    };
}
