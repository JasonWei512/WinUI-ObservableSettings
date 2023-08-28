using System.Linq.Expressions;
using Windows.Storage;

namespace NickJohn.WinUI.ObservableSettings.Test.Testers;

public class NativeSettingTester<TSettings, TSetting> : SettingTester<TSettings, TSetting>
{
    public NativeSettingTester(
        Expression<Func<TSettings, TSetting?>> settingProperty,
        params TSetting?[] settingValues)
            : base(
                settingProperty,
                (a, b) => EqualityComparer<TSetting>.Default.Equals(a, b),
                (settingProperty, settingValue) =>
                {
                    string settingKey = settingProperty.GetPropertyName();

                    if (settingValue is null)
                    {
                        Assert.IsFalse(ApplicationData.Current.LocalSettings.Values.ContainsKey(settingKey));
                    }
                    else
                    {
                        Assert.AreEqual(ApplicationData.Current.LocalSettings.Values[settingKey], settingValue);
                    }
                },
                settingValues)
    {

    }
}