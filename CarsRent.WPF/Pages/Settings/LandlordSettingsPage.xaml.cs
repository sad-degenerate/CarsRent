using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class LandlordSettingsPage : Page
    {
        public LandlordSettingsPage()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            var settingsSerializator = new SettingsSerializator<LandlordSettings>();
            var landlordSettings = settingsSerializator.Deserialize();

            if (landlordSettings != null)
            {
                lbxLandlord.ItemsSource = landlordSettings.Landlords;
                lbxLandlord.SelectedItem = landlordSettings.CurrentLandlord;
            }
        }

        private void Save(LandlordSettings settings)
        {
            var settingsSerializator = new SettingsSerializator<LandlordSettings>();
            settingsSerializator.Serialize(settings);
            lbxLandlord.ItemsSource = settings.Landlords;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbxLandlord.SelectedItem is Human)
            {
                var settingsSerializator = new SettingsSerializator<LandlordSettings>();
                var landlordSettings = settingsSerializator.Deserialize();
                landlordSettings.CurrentLandlord = lbxLandlord.SelectedItem as Human;

                Save(landlordSettings);
            }

            lblErrorFirst.Content = string.Empty;
            lblDoneFirst.Content = "Арендодатель успешно выбран.";
        }

        private void tbxSearchLandlord_TextChanged(object sender, TextChangedEventArgs e)
        {
            var settingsSerializator = new SettingsSerializator<LandlordSettings>();
            var landlordSettings = settingsSerializator.Deserialize();

            if (landlordSettings == null)
            {
                return;
            }

            var landlords = landlordSettings.Landlords;
            var landlordsResult = new List<Human>();
            var words = tbxSearchLandlord.Text.Split(' ');

            foreach (var landlord in landlords)
            {
                var landlordText = landlord.ToString();

                var addToResult = true;
                foreach (var word in words)
                {
                    if (landlordText.Contains(word) == false)
                    {
                        addToResult = false;
                        break;
                    }
                }

                if (addToResult == true)
                {
                    landlordsResult.Add(landlord);
                }
            }

            lbxLandlord.ItemsSource = landlordsResult.ToList();
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var settingsSerializator = new SettingsSerializator<LandlordSettings>();
            var landlordSettings = settingsSerializator.Deserialize();

            DateTime.TryParse(tbxBirthDate.Text, out var birthDate);
            DateTime.TryParse(tbxIssuingDate.Text, out var issuingDate);

            var landlord = new Human
            {
                Surname = tbxSurname.Text,
                Name = tbxName.Text,
                Patronymic = tbxPatronymic.Text,
                IdentityNumber = tbxPassportNumber.Text,
                IssuingOrganization = tbxIssuingOrganization.Text,
                RegistrationPlace = tbxRegistrationPlace.Text,
                PhoneNumber = tbxPhone.Text,
                BirthDate = birthDate,
                IssuingDate = issuingDate
            };

            if (landlordSettings.Landlords.Count == 0)
            {
                landlord.Id = 0;
            }
            else
            {
                landlord.Id = landlordSettings.Landlords.Last().Id + 1;
            }

            landlordSettings.Landlords.Add(landlord);
            landlordSettings.CurrentLandlord = landlord;

            var context = new ValidationContext(landlordSettings);
            var errors = landlordSettings.Validate(context);

            if (errors.Any() == true)
            {
                lblDone.Content = string.Empty;
                lblError.Content = errors.First();
            }
            else
            {
                lblError.Content = string.Empty;
                lblDone.Content = "Новый арендодатель успешно добавлен.";
                Save(landlordSettings);
            }
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbxLandlord.SelectedItem is Human)
            {
                var settingsSerializator = new SettingsSerializator<LandlordSettings>();
                var landlordSettings = settingsSerializator.Deserialize();
                
                if (landlordSettings.Landlords.Count <= 1)
                {
                    lblErrorFirst.Content = "Нельзя удалить последнего арендодателя, перед этим создайте нового.";
                    lblDoneFirst.Content = string.Empty;
                    return;
                }

                var selectedLandlord = lbxLandlord.SelectedItem as Human;
                foreach (var landlord in landlordSettings.Landlords)
                {
                    if (landlord.Id == selectedLandlord.Id)
                    {
                        landlordSettings.Landlords.Remove(landlord);
                        break;
                    }
                }

                Save(landlordSettings);
            }

            lblErrorFirst.Content = string.Empty;
            lblDoneFirst.Content = "Арендодатель успешно удален.";
        }
    }
}