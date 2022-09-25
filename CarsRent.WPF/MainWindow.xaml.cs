using CarsRent.WPF.Pages.Settings;
using CarsRent.WPF.Pages.MainFramePages;
using System.Windows;
using CarsRent.LIB.DataBase;
using System;
using CarsRent.LIB.Settings;
using CarsRent.LIB.Model;
using System.Collections.Generic;

namespace CarsRent.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartupDataBaseConnection();

            CheckSettings();
        }

        private void StartupDataBaseConnection()
        {
            ApplicationContext.Instance();
        }

        private void CheckSettings()
        {
            var displaySerializator = new SettingsSerializator<DisplaySettings>();
            var displaySettings = displaySerializator.Deserialize();
            if (displaySettings == null)
            {
                displaySettings = new DisplaySettings()
                {
                    TableOnePageElementsCount = 10
                };
                displaySerializator.Serialize(displaySettings);
            }

            var landlordSerializator = new SettingsSerializator<LandlordSettings>();
            var landlordSettings = landlordSerializator.Deserialize();

            if (landlordSettings == null)
            {
                var landlord = new Human
                {
                    Surname = "Фамилия",
                    Name = "Имя",
                    Patronymic = "Отчество",
                    IdentityNumber = "3434343343",
                    IssuingDate = DateTime.Now,
                    PhoneNumber = "88888888888",
                    RegistrationPlace = "г. Город ул. Улица д. 1 кв. 1",
                    BirthDate = DateTime.Now,
                };

                landlordSettings = new LandlordSettings();
                landlordSettings.Landlords = new List<Human>
                {
                    landlord,
                };
                landlordSettings.CurrentLandlord = landlordSettings.Landlords[0];
                landlordSerializator.Serialize(landlordSettings);
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow
            {
                Owner = this
            };
            window.Show();
        }

        private void btnRenters_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Renters();
        }

        private void btnCars_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Cars();
        }

        private void btnContracts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Contracts();
        }

        private void mainFrame_ContentRendered(object sender, EventArgs e)
        {
            mainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}