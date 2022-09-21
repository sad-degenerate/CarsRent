using CarsRent.LIB.Settings;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class DisplaySettingsPage : Page
    {
        public DisplaySettingsPage()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var serializator = new SettingsSerializator<DisplaySettings>();
            var settings = serializator.Deserialize();

            if (settings != null)
            {
                tbxTableOnePageElCount.Text = settings.TableOnePageElementsCount.ToString();
            }
        }

        private void Save()
        {
            var settings = new DisplaySettings();

            if (int.TryParse(tbxTableOnePageElCount.Text, out int onePageCount) == false)
            {
                lblError.Content = "Не удалось преобразовать в число.";
                lblDone.Content = string.Empty;
                return;
            }

            settings.TableOnePageElementsCount = onePageCount;

            var context = new ValidationContext(settings);
            var errors  = settings.Validate(context);

            if (errors.Any() == true)
            {
                lblDone.Content = string.Empty;
                lblError.Content = errors.First();
            }
            else
            {
                lblError.Content = string.Empty;
                lblDone.Content = "Успешно изменено/добавлено";
                var serializator = new SettingsSerializator<DisplaySettings>();
                serializator.Serialize(settings);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}