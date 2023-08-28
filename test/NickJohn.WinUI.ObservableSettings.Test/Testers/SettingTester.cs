using FluentAssertions;
using FluentAssertions.Events;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace NickJohn.WinUI.ObservableSettings.Test.Testers;

public abstract class SettingTester<TSettings>
{
    public abstract void Test(TSettings settings);
}

public class SettingTester<TSettings, TSetting> : SettingTester<TSettings>
{
    private readonly Expression<Func<TSettings, TSetting?>> settingProperty;
    private readonly SettingEqualityComparer<TSetting> settingComparer;
    private readonly Action<Expression<Func<TSettings, TSetting?>>, TSetting?> additionalAssert;
    private readonly TSetting?[] settingValues;

    public SettingTester(
        Expression<Func<TSettings, TSetting?>> settingProperty,
        Func<TSetting, TSetting, bool> settingComparer,
        Action<Expression<Func<TSettings, TSetting?>>, TSetting?> additionalAssert,
        params TSetting?[] settingValues)
    {
        this.settingProperty = settingProperty;
        this.settingComparer = new SettingEqualityComparer<TSetting>(settingComparer);
        this.additionalAssert = additionalAssert;
        this.settingValues = settingValues;
    }

    public override void Test(TSettings settings)
    {
        Func<TSettings, TSetting?> settingPropertyGetter = settingProperty.Compile();
        string settingPropertyName = settingProperty.GetPropertyName();

        foreach (TSetting? settingValue in settingValues)
        {
            using IMonitor<TSettings> monitor = settings.Monitor();

            // Get previous setting value
            TSetting? previousSettingValue = settingPropertyGetter(settings);

            // Set setting value
            settings.SetProperty(settingProperty, settingValue);

            // Get current setting value
            TSetting? currentSettingValue = settingPropertyGetter(settings);

            // Assert setting are set as expected
            Assert.AreEqual(settingValue, currentSettingValue, settingComparer);
            additionalAssert(settingProperty, settingValue);

            // Assert events are raised
            if (!EqualityComparer<TSetting?>.Default.Equals(previousSettingValue, currentSettingValue))
            {
                monitor
                    .Should().Raise("PropertyChanged")
                    .WithSender(settings)
                    .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == settingPropertyName);

                monitor
                    .Should().Raise($"{settingPropertyName}Changed")
                    .WithSender(settings)
                    .WithArgs<SettingValueChangedEventArgs<TSetting>>(args =>
                        settingComparer.Equals(args.OldValue, previousSettingValue) &&
                        settingComparer.Equals(args.NewValue, currentSettingValue));
            }
            else
            {
                monitor.Should().NotRaise("PropertyChanged");
                monitor.Should().NotRaise($"{settingPropertyName}Changed");
            }
        }
    }
}

public class SettingEqualityComparer<T> : IEqualityComparer<T?>
{
    private readonly Func<T, T, bool> equalityComparer;

    public SettingEqualityComparer(Func<T, T, bool> equalityComparer)
    {
        this.equalityComparer = equalityComparer;
    }

    public bool Equals(T? x, T? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is not null && y is not null && equalityComparer(x, y))
        {
            return true;
        }

        return false;
    }

    public int GetHashCode([DisallowNull] T obj)
    {
        return obj.GetHashCode();
    }
}
