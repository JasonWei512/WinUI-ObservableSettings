# WinUI ObservableSettings

[![Nuget](https://img.shields.io/nuget/v/NickJohn.WinUI.ObservableSettings)](https://www.nuget.org/packages/NickJohn.WinUI.ObservableSettings)
[![CI](https://github.com/JasonWei512/WinUI-ObservableSettings/actions/workflows/CI.yml/badge.svg)](https://github.com/JasonWei512/WinUI-ObservableSettings/actions/workflows/CI.yml)

A C# source generator to help you generate boilerplates to read and write settings in [Windows.Storage.ApplicationData.Current.LocalSettings](https://learn.microsoft.com/en-us/windows/apps/design/app-settings/store-and-retrieve-app-data#retrieve-the-local-app-data-store) in WinUI 3.

It will generate a partial class that:
- Has strong-typed properties to read and write settings in storage
- Implements `INotifyPropertyChanged` so you can bind to it in XAML
- Raises an event when setting value changes


# Quickstart

1.  Install `NickJohn.WinUI.ObservableSettings` from [Nuget](https://www.nuget.org/packages/NickJohn.WinUI.ObservableSettings).

2.  Say you want to store a `Volume` as `double` in storage, with the default value `0.75`.

    All you need to do is adding an `[ObservableSetting]` attribute to the default value field:

    ```csharp
    using NickJohn.WinUI.ObservableSettings;
    ...
    public partial class SettingsService    // Don't forget to add "partial" keyword to the class!
    {
        [ObservableSetting("Volume")]   // The "Volume" here is the key of the setting in storage
        private readonly double volume = 0.75;  // This field is used as the default setting value
    }
    ```

    It will generate a partial class:

    ```csharp
    public partial class SettingsService : INotifyPropertyChanged
    {
        // You can bind to "Volume" in XAML
        public event PropertyChangedEventHandler? PropertyChanged;

        // When the setting "Volume" changes, this event will be raised
        public event EventHandler<SettingValueChangedEventArgs<double>>? VolumeChanged;

        // Strong typed "Volume" property to read and write setting in storage
        public double Volume 
        {
            get { ... } // Read setting from storage
            set { ... } // Write setting to storage
        }
    }
    ```

3.  Now you can use the generated class to read and write settings:

    ```csharp
    SettingsService settingsService = new SettingsService();

    // Handle setting value changed events
    settingsService.VolumeChanged += (s, e) => 
    {
        Debug.WriteLine($"Volume changed from {e.OldValue} to {e.NewValue}");
    }

    Volume volume = settingsService.Volume; // Read settings from storage

    volume = volume / 2;

    settingsService.Volume = volume // Write settings to storage
    ```


# Details

## How does the generated class look like?

Basically like this:

```csharp
public partial class SettingsService : INotifyPropertyChanged
{
    private IPropertySet LocalSettings => Windows.Storage.ApplicationData.Current.LocalSettings.Values;

    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler<SettingValueChangedEventArgs<double>>? VolumeChanged;
        
    public double Volume
    {
        get
        {
            if (LocalSettings.TryGetValue("Volume", out object? settingObject))
            {
                if (settingObject is double settingValue)
                {
                    return settingValue;
                }
            }
            return volume;
        }
        set
        {
            double oldValue = Volume;
            if (!EqualityComparer<double>.Default.Equals(oldValue, value))
            {
                LocalSettings["Volume"] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Volume"));
                VolumeChanged?.Invoke(this, new SettingValueChangedEventArgs<double>(oldValue, value));
            }
        }
    }
}
```

## Provide an explicit setting key

- It's highly recommended to provide an explicit `settingKey` to the `[ObservableSetting]` attribute.

  ```csharp
  [ObservableSetting("UserEmail")]
  private readonly string userEmail = "";
  ```

- If you don't, the Pascal form of the attributed field name will be used (same as the generated property name). 

  ```csharp
  [ObservableSetting] // Setting key is "UserEmail"
  private readonly string userEmail = "";
  ```
  
  ⚠ But if you don't provide an explicit `settingKey`, when you renames the attributed field, the setting key will change, and the saved setting will not be read correctly!

## How are the settings stored?

[Some types](https://learn.microsoft.com/en-us/windows/apps/design/app-settings/store-and-retrieve-app-data#settings) can be directly stored in `Windows.Storage.ApplicationData.Current.LocalSettings`.

- If the type of the setting to save is one of these "native" setting types, it will be directly stored in storage.

- Otherwise, it will be serialized as JSON with `System.Text.Json` and saved as `string`.

## Setting size limit

According to the [official documents](https://learn.microsoft.com/en-us/uwp/api/windows.storage.applicationdata.localsettings#remarks):

- Each setting key can be 255 characters in length at most.
- Each setting can be up to 8K bytes in size.

# Acknowledgements

This project is inspired by:
- https://github.com/joseangelmt/ObservableSettings
- [Microsoft MVVM Toolkit Source Generators](https://github.com/CommunityToolkit/dotnet)
