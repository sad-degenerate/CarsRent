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
                tbxSurname.Text = dataSettings.Surname;
                tbxName.Text = dataSettings.Name;
                tbxPatronymic.Text = dataSettings.Patronymic;
                tbxBirthDate.Text = dataSettings.BirthDate;
                tbxPassportNumber.Text = dataSettings.PassportNumber;
                tbxIssuingOrganization.Text = dataSettings.IssuingOrganization;
                tbxIssuingDate.Text = dataSettings.IssuingDate;
                tbxRegistrationPlace.Text = dataSettings.RegistrationPlace;

                tbxActSample.Text = dataSettings.ActSample;
                tbxContractSample.Text = dataSettings.ContractSample;
                tbxNotificationSample.Text = dataSettings.NotificationSample;
                tbxOutputFolder.Text = dataSettings.OutputFolder;
            }
        }

        private void Save()
        {
            var surname = tbxSurname.Text;
            var name = tbxName.Text;
            var patronymic = tbxPatronymic.Text;
            var birthDate = tbxBirthDate.Text;
            var passportNumber = tbxPassportNumber.Text;
            var issuingOrganization = tbxIssuingOrganization.Text;
            var issuingDate = tbxIssuingDate.Text;
            var registrationPlace = tbxRegistrationPlace.Text;
            
            var actSample = tbxActSample.Text;
            var contractSample = tbxContractSample.Text;
            var notificationSample = tbxNotificationSample.Text;
            var outputFolder = tbxOutputFolder.Text;

            var dataSettings = new TemplatesSettings(surname, name, patronymic, birthDate, passportNumber, issuingOrganization
                                               , issuingDate, registrationPlace, actSample, contractSample, notificationSample, outputFolder);

            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            settingsSerializator.Serialize(dataSettings);
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Save();
        }
    }
}