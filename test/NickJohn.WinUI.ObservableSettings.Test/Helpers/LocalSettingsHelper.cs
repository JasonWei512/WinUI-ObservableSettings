using Windows.Storage;

namespace NickJohn.WinUI.ObservableSettings.Test.Helpers;

public static class LocalSettingsHelper
{
    public static void DeleteAllSettings()
    {
        ApplicationData.Current.LocalSettings.Values.Clear();
    }
}
