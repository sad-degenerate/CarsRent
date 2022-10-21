using CarsRent.LIB.Settings;
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
            var settings = SettingsCommands<DisplaySettings>.GetSettings();
            tbxTableOnePageElCount.Text = settings.TableOnePageElementsCount.ToString();
        }

        private void Save()
        {
            if (int.TryParse(tbxTableOnePageElCount.Text, out var onePageCount) == false)
            {
                lblError.Content = "Не удалось преобразовать в число.";
                lblDone.Content = string.Empty;
                return;
            }

            var settings = new DisplaySettings
            {
                TableOnePageElementsCount = onePageCount
            };

            var error = SettingsCommands<DisplaySettings>.SaveSettings(settings);

            if (string.IsNullOrWhiteSpace(error))
            {
                lblDone.Content = "Настрйки успешно сохранены.";
                lblError.Content = string.Empty;
            }

            lblDone.Content = string.Empty;
            lblError.Content = error;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}