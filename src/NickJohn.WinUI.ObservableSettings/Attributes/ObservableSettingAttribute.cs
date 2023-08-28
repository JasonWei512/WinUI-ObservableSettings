namespace NickJohn.WinUI.ObservableSettings;

/// <summary>
/// A C# source generator attribute to help you generate boilerplates to read and write settings in
/// <see href="https://learn.microsoft.com/en-us/windows/apps/design/app-settings/store-and-retrieve-app-data#retrieve-the-local-app-data-store">
///     Windows.Storage.ApplicationData.Current.LocalSettings
/// </see> 
/// in WinUI. <br/> <br/>
/// 
/// Say you want to store a <c>Volume</c> as <c>double</c> in storage, with the default value <c>0.75</c>. <br/>
/// All you need to do is adding an <c>[ObservableSetting]</c> attribute to the default value field:
/// <code>
/// using NickJohn.WinUI.ObservableSettings;
/// ...
/// public partial class SettingsService    // Don't forget to add "partial" keyword to the class!
/// {
///     [ObservableSetting("Volume")]   // The "Volume" here is the key of the setting in storage
///     private readonly double volume = 0.75;  // This field is used as the default setting value
/// }
/// </code>
/// 
/// It will generate a partial class:
/// <code>
/// public partial class SettingsService : INotifyPropertyChanged
/// {
///     // You can bind to "Volume" in XAML
///     public event PropertyChangedEventHandler? PropertyChanged;
/// 
///     // When the setting "Volume" changes, this event will be raised
///     public event EventHandler&lt;SettingValueChangedEventArgs&lt;double&gt;&gt;? VolumeChanged;
///     
///     // Strong typed "Volume" property to read and write setting in storage
///     public double Volume
///     {
///         get { ... } // Read setting from storage
///         set { ... } // Write setting to storage
///     }
/// }
/// </code>
/// 
/// Now you can use the generated class to read and write settings:
/// <code>
/// SettingsService settingsService = new SettingsService();
/// // Handle setting value changed events
/// settingsService.VolumeChanged += (s, e) => 
/// {
///     Debug.WriteLine($"Volume changed from {e.OldValue} to {e.NewValue}");
/// }
/// 
/// Volume volume = settingsService.Volume; // Read settings from storage
/// 
/// volume = volume / 2;
/// 
/// settingsService.Volume = volume // Write settings to storage
/// </code>
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class ObservableSettingAttribute : Attribute
{
    /// <param name="settingKey">
    /// The key of the setting in storage.
    /// 
    /// <para>
    /// It's highly recommended to provide an explicit <c>settingKey</c> to the <c>[ObservableSetting]</c> attribute.
    /// <code>
    /// [ObservableSetting("UserEmail")]
    /// private readonly string userEmail = "";
    /// </code>
    /// </para>
    /// 
    /// <para>
    /// If you don't, the Pascal form of the attributed field name will be used (same as the generated property name). 
    /// <code>
    /// [ObservableSetting] // Setting key is "UserEmail"
    /// private readonly string userEmail = "";
    /// </code>
    /// ⚠ But if you don't provide an explicit <c>settingKey</c>, when you renames the attributed field, the setting key will change, 
    /// and the saved setting will not be read correctly!
    /// </para>
    /// </param>
    /// 
    /// <param name="raiseEvent">
    /// Whether to raise setting changed event or not. For example, the code:
    /// <code>
    /// [ObservableSetting(raiseEvent: false)]
    /// private readonly double volume = 0.75;
    /// </code>
    /// will not raise <c>VolumeChanged</c> event when the value of <c>Volume</c> changes.
    /// </param>
    public ObservableSettingAttribute(string? settingKey = null, bool raiseEvent = true)
    {
    }
}
