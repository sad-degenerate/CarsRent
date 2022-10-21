using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Settings;

public static class SettingsCommands<T> where T : SettingsBase, IValidatableObject
{
    public static T GetSettings()
    {
        var serializator = new SettingsSerializator<T>();
        var settings = serializator.Deserialize();

        if (settings != null)
        {
            return settings;
        }
        
        settings = settings.Default() as T;
        SaveSettings(settings);
        return settings;
    }

    public static string SaveSettings(T settings)
    {
        var context = new ValidationContext(settings);
        var errors = settings.Validate(context);

        if (errors.Any())
        {
            return errors.First().ErrorMessage;
        }

        var serializator = new SettingsSerializator<T>();
        serializator.Serialize(settings);
        
        return string.Empty;
    }
}