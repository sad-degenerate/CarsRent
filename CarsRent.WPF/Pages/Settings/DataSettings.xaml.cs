using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Settings;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class DataSettings : Page
    {
        public DataSettings()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var dataSettings = settingsSerializator.Deserialize();

            if (dataSettings != null)
            {
                tbxActSample.Text = dataSettings.ActSample;
                tbxContractSample.Text = dataSettings.ContractSample;
                tbxNotificationSample.Text = dataSettings.NotificationSample;
                tbxOutputFolder.Text = dataSettings.OutputFolder;
            }
        }

        private void Save()
        {
            var dataSettings = new TemplatesSettings
            {
                ActSample = tbxActSample.Text,
                ContractSample = tbxContractSample.Text,
                NotificationSample = tbxNotificationSample.Text,
                OutputFolder = tbxOutputFolder.Text
            };

            var context = new ValidationContext(dataSettings);
            var errors = dataSettings.Validate(context);

            if (errors.Any() == true)
            {
                lblDone.Content = string.Empty;
                lblError.Content = errors.First();
            }
            else
            {
                lblError.Content = string.Empty;
                lblDone.Content = "Успешно изменено/добавлено";
                var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
                settingsSerializator.Serialize(dataSettings);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}