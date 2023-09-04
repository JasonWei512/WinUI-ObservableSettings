// Note:
// Some types can be directly stored in "ApplicationData.Current.LocalSettings": 
// https://docs.microsoft.com/windows/apps/design/app-settings/store-and-retrieve-app-data#settings
// Unfortunately there's no way to constraint a generic type "T" to "T1 or T2" in C#, so I have to use a T4 template to generate these methods.

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved

using System.Text.Json;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace NickJohn.WinUI.ObservableSettings.Internal;

/// <summary>
/// A helper class to read and write settings in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/>.
/// </summary>
public static class SettingsManager
{
    private static IPropertySet LocalSettings => ApplicationData.Current.LocalSettings.Values;

    #region JSON

    /// <summary>
    /// Try to read JSON string from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>, 
    /// deserialized it to <typeparamref name="T"/> and return the value. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of the setting</typeparam>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static T GetJsonSetting<T>(string settingKey, T defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is string jsonString)
            {
                try
                {
                    T? settingValue = JsonSerializer.Deserialize<T>(jsonString);
                    if (settingValue is not null)
                    {
                        return settingValue;
                    }
                }
                catch (JsonException)
                {
                    // The JSON is invalid
                }
            }
        }

        return defaultValue;
    }

    /// <summary>
    /// Try to read JSON string from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>, 
    /// deserialized it to <typeparamref name="T"/> and return the value. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of the setting</typeparam>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static T? GetJsonSettingNullable<T>(string settingKey, T? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is string jsonString)
            {
                try
                {
                    T? settingValue = JsonSerializer.Deserialize<T>(jsonString);
                    if (settingValue is not null)
                    {
                        return settingValue;
                    }
                }
                catch (JsonException)
                {
                    // The JSON is invalid
                }
            }
        }

        return defaultValue;
    }

    /// <summary>
    /// Serialize <paramref name="value"/> to a JSON string, 
    /// and save it to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>, 
    /// </summary>
    /// <typeparam name="T">The type of the setting</typeparam>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetJsonSetting<T>(string settingKey, T? value)
    {
        string jsonString = JsonSerializer.Serialize(value);
        LocalSettings[settingKey] = jsonString;
    }

    #endregion

    #region System.Int16

    /// <summary>
    /// Try to read a <see cref="System.Int16"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Int16 GetNativeSetting(string settingKey, System.Int16 defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Int16 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Int16"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Int16? GetNativeSettingNullable(string settingKey, System.Int16? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Int16 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Int16? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.UInt16

    /// <summary>
    /// Try to read a <see cref="System.UInt16"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.UInt16 GetNativeSetting(string settingKey, System.UInt16 defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.UInt16 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.UInt16"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.UInt16? GetNativeSettingNullable(string settingKey, System.UInt16? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.UInt16 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.UInt16? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Int32

    /// <summary>
    /// Try to read a <see cref="System.Int32"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Int32 GetNativeSetting(string settingKey, System.Int32 defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Int32 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Int32"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Int32? GetNativeSettingNullable(string settingKey, System.Int32? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Int32 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Int32? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.UInt32

    /// <summary>
    /// Try to read a <see cref="System.UInt32"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.UInt32 GetNativeSetting(string settingKey, System.UInt32 defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.UInt32 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.UInt32"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.UInt32? GetNativeSettingNullable(string settingKey, System.UInt32? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.UInt32 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.UInt32? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Int64

    /// <summary>
    /// Try to read a <see cref="System.Int64"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Int64 GetNativeSetting(string settingKey, System.Int64 defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Int64 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Int64"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Int64? GetNativeSettingNullable(string settingKey, System.Int64? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Int64 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Int64? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.UInt64

    /// <summary>
    /// Try to read a <see cref="System.UInt64"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.UInt64 GetNativeSetting(string settingKey, System.UInt64 defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.UInt64 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.UInt64"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.UInt64? GetNativeSettingNullable(string settingKey, System.UInt64? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.UInt64 settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.UInt64? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Single

    /// <summary>
    /// Try to read a <see cref="System.Single"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Single GetNativeSetting(string settingKey, System.Single defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Single settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Single"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Single? GetNativeSettingNullable(string settingKey, System.Single? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Single settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Single? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Double

    /// <summary>
    /// Try to read a <see cref="System.Double"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Double GetNativeSetting(string settingKey, System.Double defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Double settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Double"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Double? GetNativeSettingNullable(string settingKey, System.Double? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Double settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Double? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Boolean

    /// <summary>
    /// Try to read a <see cref="System.Boolean"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Boolean GetNativeSetting(string settingKey, System.Boolean defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Boolean settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Boolean"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Boolean? GetNativeSettingNullable(string settingKey, System.Boolean? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Boolean settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Boolean? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Char

    /// <summary>
    /// Try to read a <see cref="System.Char"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Char GetNativeSetting(string settingKey, System.Char defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Char settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Char"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Char? GetNativeSettingNullable(string settingKey, System.Char? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Char settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Char? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.String

    /// <summary>
    /// Try to read a <see cref="System.String"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.String GetNativeSetting(string settingKey, System.String defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.String settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.String"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.String? GetNativeSettingNullable(string settingKey, System.String? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.String settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.String? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.DateTimeOffset

    /// <summary>
    /// Try to read a <see cref="System.DateTimeOffset"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.DateTimeOffset GetNativeSetting(string settingKey, System.DateTimeOffset defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.DateTimeOffset settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.DateTimeOffset"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.DateTimeOffset? GetNativeSettingNullable(string settingKey, System.DateTimeOffset? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.DateTimeOffset settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.DateTimeOffset? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.TimeSpan

    /// <summary>
    /// Try to read a <see cref="System.TimeSpan"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.TimeSpan GetNativeSetting(string settingKey, System.TimeSpan defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.TimeSpan settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.TimeSpan"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.TimeSpan? GetNativeSettingNullable(string settingKey, System.TimeSpan? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.TimeSpan settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.TimeSpan? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region System.Guid

    /// <summary>
    /// Try to read a <see cref="System.Guid"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Guid GetNativeSetting(string settingKey, System.Guid defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Guid settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="System.Guid"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static System.Guid? GetNativeSettingNullable(string settingKey, System.Guid? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is System.Guid settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, System.Guid? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region Windows.Foundation.Point

    /// <summary>
    /// Try to read a <see cref="Windows.Foundation.Point"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static Windows.Foundation.Point GetNativeSetting(string settingKey, Windows.Foundation.Point defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is Windows.Foundation.Point settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="Windows.Foundation.Point"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static Windows.Foundation.Point? GetNativeSettingNullable(string settingKey, Windows.Foundation.Point? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is Windows.Foundation.Point settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, Windows.Foundation.Point? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region Windows.Foundation.Size

    /// <summary>
    /// Try to read a <see cref="Windows.Foundation.Size"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static Windows.Foundation.Size GetNativeSetting(string settingKey, Windows.Foundation.Size defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is Windows.Foundation.Size settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="Windows.Foundation.Size"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static Windows.Foundation.Size? GetNativeSettingNullable(string settingKey, Windows.Foundation.Size? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is Windows.Foundation.Size settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, Windows.Foundation.Size? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

    #region Windows.Foundation.Rect

    /// <summary>
    /// Try to read a <see cref="Windows.Foundation.Rect"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static Windows.Foundation.Rect GetNativeSetting(string settingKey, Windows.Foundation.Rect defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is Windows.Foundation.Rect settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Try to read a <see cref="Windows.Foundation.Rect"/> from <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>. <br/>
    /// If failed, return <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting in <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/></param>
    /// <param name="defaultValue">The default setting value to return if failed to read setting</param>
    public static Windows.Foundation.Rect? GetNativeSettingNullable(string settingKey, Windows.Foundation.Rect? defaultValue)
    {
        if (LocalSettings.TryGetValue(settingKey, out object? settingObject))
        {
            if (settingObject is Windows.Foundation.Rect settingValue)
            {
                return settingValue;
            }
        }
        return defaultValue;
    }

    /// <summary>
    /// Save <paramref name="value"/> to <see cref="Windows.Storage.ApplicationData.Current.LocalSettings.Values"/> with <paramref name="settingKey"/>.
    /// </summary>
    /// <param name="settingKey">The key of setting</param>
    /// <param name="value">The setting value to set</param>
    public static void SetNativeSetting(string settingKey, Windows.Foundation.Rect? value)
    {
        LocalSettings[settingKey] = value;
    }

    #endregion

}

#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved