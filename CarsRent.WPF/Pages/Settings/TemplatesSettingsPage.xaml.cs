using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Settings;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class TemplatesSettingsPage : Page
    {
        public TemplatesSettingsPage()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var settings = SettingsController<TemplatesSettings>.GetSettings();
            
            TbxActSample.Text = settings.ActSample;
            TbxContractSample.Text = settings.ContractSample;
            TbxNotificationSample.Text = settings.NotificationSample;
            TbxOutputFolder.Text = settings.OutputFolder;
        }

        private void Save()
        {
            var settings = new TemplatesSettings
            {
                ActSample = TbxActSample.Text,
                ContractSample = TbxContractSample.Text,
                NotificationSample = TbxNotificationSample.Text,
                OutputFolder = TbxOutputFolder.Text
            };

            var error = SettingsController<TemplatesSettings>.SaveSettings(settings);

            if (string.IsNullOrWhiteSpace(error) == false)
            {
                LblDone.Content = string.Empty;
                LblError.Content = error;
                return;
            }
            
            LblError.Content = string.Empty;
            LblDone.Content = "Настройки успешно сохранены.";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}