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
            var settings = SettingsCommands<TemplatesSettings>.GetSettings();
            
            tbxActSample.Text = settings.ActSample;
            tbxContractSample.Text = settings.ContractSample;
            tbxNotificationSample.Text = settings.NotificationSample;
            tbxOutputFolder.Text = settings.OutputFolder;
        }

        private void Save()
        {
            var settings = new TemplatesSettings
            {
                ActSample = tbxActSample.Text,
                ContractSample = tbxContractSample.Text,
                NotificationSample = tbxNotificationSample.Text,
                OutputFolder = tbxOutputFolder.Text
            };

            var error = SettingsCommands<TemplatesSettings>.SaveSettings(settings);

            if (string.IsNullOrWhiteSpace(error) == false)
            {
                lblDone.Content = string.Empty;
                lblError.Content = error;
                return;
            }
            
            lblError.Content = string.Empty;
            lblDone.Content = "Настройки успешно сохранены.";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}