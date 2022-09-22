using System.Xml.Serialization;

namespace CarsRent.LIB.Settings
{
    public class SettingsSerializator<T> where T : SettingsBase
    {
        private readonly string _path;

        public SettingsSerializator()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CarsRent\Settings";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _path = path;
        }

        public void Serialize(T settings)
        {
            var formatter = new XmlSerializer(settings.GetType());
            using var fs = new FileStream(_path + $@"\{typeof(T)}", FileMode.Create);
            formatter.Serialize(fs, settings);
        }

        public T Deserialize()
        {
            var serializer = new XmlSerializer(typeof(T));
            using var fs = new FileStream(_path + $@"\{typeof(T)}", FileMode.OpenOrCreate);

            T settings;
            try
            {
                settings = serializer.Deserialize(fs) as T;
            }
            catch (Exception ex)
            {
                settings = null;
            }

            return settings;
        }
    }
}