namespace NickJohn.WinUI.ObservableSettings.Core.Constants;

public static class SettingTypes
{
    // Some types can be directly stored in "ApplicationData.Current.LocalSettings": 
    // https://docs.microsoft.com/windows/apps/design/app-settings/store-and-retrieve-app-data#settings
    public static readonly string[] NativeSettingTypes = new[]
    {
        typeof(System.Int16), typeof(System.UInt16), typeof(System.Int32), typeof(System.UInt32), typeof(System.Int64), typeof(System.UInt64), typeof(System.Single), typeof(System.Double),
        typeof(System.Boolean),
        typeof(System.Char), typeof(System.String),
        typeof(System.DateTimeOffset), typeof(System.TimeSpan),
        typeof(System.Guid)
    }
         .Select(type => type.FullName)
         .Concat(new[]
         {
             "Windows.Foundation.Point",
             "Windows.Foundation.Size",
             "Windows.Foundation.Rect"
         })
         .ToArray();

    public static readonly string[] NativeNonPrimitiveSettingTypes = new[]
    {
        typeof(System.String)
    }
        .Select(type => type.FullName)
        .ToArray();

    public static readonly string[] NativePrimitiveSettingTypes = NativeSettingTypes
        .Where(type => !NativeNonPrimitiveSettingTypes.Contains(type))
        .ToArray();
}
