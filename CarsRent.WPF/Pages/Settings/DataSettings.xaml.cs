using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Controls;
using CarsRent.LIB.Model;
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
                tbxSurname.Text = dataSettings.Landlord.Surname;
                tbxName.Text = dataSettings.Landlord.Name;
                tbxPatronymic.Text = dataSettings.Landlord.Patronymic;
                tbxBirthDate.Text = dataSettings.Landlord.BirthDate;
                tbxPassportNumber.Text = dataSettings.Landlord.IdentityNumber;
                tbxIssuingOrganization.Text = dataSettings.Landlord.IssuingOrganization;
                tbxIssuingDate.Text = dataSettings.Landlord.IssuingDate;
                tbxRegistrationPlace.Text = dataSettings.Landlord.RegistrationPlace;
                tbxPhone.Text = dataSettings.Landlord.PhoneNumber;

                tbxActSample.Text = dataSettings.ActSample;
                tbxContractSample.Text = dataSettings.ContractSample;
                tbxNotificationSample.Text = dataSettings.NotificationSample;
                tbxOutputFolder.Text = dataSettings.OutputFolder;
            }
        }

        private void Save()
        {
            var dataSettings = new TemplatesSettings();
            var landlord = new Human();

            landlord.Surname = tbxSurname.Text;
            landlord.Name = tbxName.Text;
            landlord.Patronymic = tbxPatronymic.Text;
            landlord.BirthDate = tbxBirthDate.Text;
            landlord.IdentityNumber = tbxPassportNumber.Text;
            landlord.IssuingOrganization = tbxIssuingOrganization.Text;
            landlord.IssuingDate = tbxIssuingDate.Text;
            landlord.RegistrationPlace = tbxRegistrationPlace.Text;
            landlord.PhoneNumber = tbxPhone.Text;

            dataSettings.Landlord = landlord;

            dataSettings.ActSample = tbxActSample.Text;
            dataSettings.ContractSample = tbxContractSample.Text;
            dataSettings.NotificationSample = tbxNotificationSample.Text;
            dataSettings.OutputFolder = tbxOutputFolder.Text;

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

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Save();
        }
    }
}