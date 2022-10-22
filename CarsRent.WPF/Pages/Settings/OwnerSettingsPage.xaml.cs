﻿using CarsRent.LIB.Settings;
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
            LbxOwner.ItemsSource = OwnersSettings.GetOwners(TbxSearchOwner.Text, 0, 10).ToList();
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOwners();
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fields = new Dictionary<string, string>
            {
                { "surname", TbxSurname.Text },
                { "name", TbxName.Text },
                { "patronymic", TbxPatronymic.Text },
                { "passportNumber", TbxPassportNumber.Text },
                { "issuingOrganization", TbxIssuingOrganization.Text },
                { "registrationPlace", TbxRegistrationPlace.Text },
                { "phone", TbxPhone.Text },
                { "birthDate", TbxBirthDate.Text },
                { "issuingDate", TbxIssuingDate.Text }
            };

            var error = OwnersSettings.AddOwner(fields);

            if (string.IsNullOrWhiteSpace(error))
            {
                LblAddError.Content = string.Empty;
                LblAddDone.Content = "Арендодатель успешно добавлен.";
                UpdateOwners();
                return;
            }

            LblAddDone.Content = string.Empty;
            LblAddError.Content = error;
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var error = OwnersSettings.DeleteOwner(LbxOwner.SelectedItem);
            
            if (string.IsNullOrWhiteSpace(error))
            {
                UpdateOwners();
                LblChooseError.Content = string.Empty;
                LblChooseDone.Content = "Арендодатель успешно удален.";
                return;
            }

            LblChooseDone.Content = string.Empty;
            LblChooseError.Content = error;
        }
    }
}