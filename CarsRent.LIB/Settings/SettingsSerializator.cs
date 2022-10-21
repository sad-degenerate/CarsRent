using System.Text.Json;

namespace CarsRent.LIB.Settings
{
    public class SettingsSerializator<T> where T : SettingsBase
    {
        private readonly string _path;

        public SettingsSerializator()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CarsRent\Settings";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            _path = path;
        }

        public void Serialize(T settings)
        {
            using var fs = File.Create(_path + $@"\{typeof(T)}");
            JsonSerializer.SerializeAsync(fs, settings);
        }

        public T? Deserialize()
        {
            T? settings;
            try
            {
                using var fs = File.OpenRead(_path + $@"\{typeof(T)}");
                settings = JsonSerializer.Deserialize<T>(fs);
            }
            catch (Exception) 
            { 
                settings = null;
            }

            return settings;
        }
    }
}