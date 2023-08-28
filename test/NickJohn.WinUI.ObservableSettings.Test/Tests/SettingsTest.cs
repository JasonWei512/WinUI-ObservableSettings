using NickJohn.WinUI.ObservableSettings.Test.Testers;
using System.Linq.Expressions;

namespace NickJohn.WinUI.ObservableSettings.Test.Tests;

public abstract class SettingsTest<TSettings>
{
    [TestInitialize]
    public void TestInitialize()
    {
        LocalSettingsHelper.DeleteAllSettings();
    }

    protected static SettingTester<TSettings> SettingTester<TSetting>(
        Expression<Func<TSettings, TSetting?>> settingProperty,
        Func<TSetting, TSetting, bool> settingComparer,
        Action<Expression<Func<TSettings, TSetting?>>, TSetting?> additionalAssert,
        params TSetting?[] settingValues)
    {
        return new SettingTester<TSettings, TSetting>(
            settingProperty,
            settingComparer,
            additionalAssert,
            settingValues);
    }

    protected static SettingTester<TSettings> NativeSettingTester<TSetting>(
        Expression<Func<TSettings, TSetting?>> settingProperty,
        params TSetting?[] settingValues)
    {
        return new NativeSettingTester<TSettings, TSetting>(
            settingProperty,
            settingValues);
    }

    protected static SettingTester<TSettings> JsonSettingTester<TSetting>(
        Expression<Func<TSettings, TSetting?>> settingProperty,
        Func<TSetting, TSetting, bool> settingComparer,
        params TSetting?[] settingValues)
    {
        return new JsonSettingTester<TSettings, TSetting>(
            settingProperty,
            settingComparer,
            settingValues);
    }
}