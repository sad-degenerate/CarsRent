using CarsRent.LIB.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class OwnerSettingsPage : Page
    {
        public OwnerSettingsPage()
        {
            InitializeComponent();
            UpdateOwners();
        }

        private void UpdateOwners()
        {
            lbxOwner.ItemsSource = OwnersSettings.GetOwners(tbxSearchOwner.Text, 0, 10).ToList();
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOwners();
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fields = new Dictionary<string, string>
            {
                { "surname", tbxSurname.Text },
                { "name", tbxName.Text },
                { "patronymic", tbxPatronymic.Text },
                { "passportNumber", tbxPassportNumber.Text },
                { "issuingOrganization", tbxIssuingOrganization.Text },
                { "registrationPlace", tbxRegistrationPlace.Text },
                { "phone", tbxPhone.Text },
                { "birthDate", tbxBirthDate.Text },
                { "issuingDate", tbxIssuingDate.Text }
            };

            var error = OwnersSettings.AddOwner(fields);

            if (string.IsNullOrWhiteSpace(error))
            {
                lblAddError.Content = string.Empty;
                lblAddDone.Content = "Арендодатель успешно добавлен.";
                UpdateOwners();
                return;
            }

            lblAddDone.Content = string.Empty;
            lblAddError.Content = error;
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var error = OwnersSettings.DeleteOwner(lbxOwner.SelectedItem);
            
            if (string.IsNullOrWhiteSpace(error))
            {
                UpdateOwners();
                lblChooseError.Content = string.Empty;
                lblChooseDone.Content = "Арендодатель успешно удален.";
                return;
            }

            lblChooseDone.Content = string.Empty;
            lblChooseError.Content = error;
        }
    }
}