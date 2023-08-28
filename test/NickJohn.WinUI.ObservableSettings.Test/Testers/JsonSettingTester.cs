using System.Linq.Expressions;
using System.Text.Json;
using Windows.Storage;

namespace NickJohn.WinUI.ObservableSettings.Test.Testers;

public class JsonSettingTester<TSettings, TSetting> : SettingTester<TSettings, TSetting>
{
    public JsonSettingTester(
        Expression<Func<TSettings, TSetting?>> settingProperty,
        Func<TSetting, TSetting, bool> settingComparer,
        params TSetting?[] settingValues)
            : base(
                settingProperty,
                settingComparer,
                (settingProperty, settingValue) =>
                {
                    string settingKey = settingProperty.GetPropertyName();

                    string settingJsonValue = JsonSerializer.Serialize(settingValue);

                    Assert.AreEqual(ApplicationData.Current.LocalSettings.Values[settingKey], settingJsonValue);
                },
                settingValues)
    {

    }
}
