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
            TbxTableOnePageElCount.Text = settings.TableOnePageElementsCount.ToString();
        }

        private void Save()
        {
            if (int.TryParse(TbxTableOnePageElCount.Text, out var onePageCount) == false)
            {
                LblError.Content = "Не удалось преобразовать в число.";
                LblDone.Content = string.Empty;
                return;
            }

            var settings = new DisplaySettings
            {
                TableOnePageElementsCount = onePageCount
            };

            var error = SettingsCommands<DisplaySettings>.SaveSettings(settings);

            if (string.IsNullOrWhiteSpace(error))
            {
                LblDone.Content = "Настрйки успешно сохранены.";
                LblError.Content = string.Empty;
            }

            LblDone.Content = string.Empty;
            LblError.Content = error;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}