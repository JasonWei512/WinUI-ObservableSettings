namespace NickJohn.WinUI.ObservableSettings;

/// <summary>
/// An EventArgs that contains the old value and the new value before and after the setting changes.
/// </summary>
/// <typeparam name="T">The type of the setting.</typeparam>
public class SettingValueChangedEventArgs<T> : EventArgs
{
    /// <summary>
    /// The old value of the setting.
    /// </summary>
    public T OldValue { get; private set; }

    /// <summary>
    /// The new value of the setting.
    /// </summary>
    public T NewValue { get; private set; }

    public SettingValueChangedEventArgs(T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}
