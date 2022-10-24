using CarsRent.WPF.Pages.Settings;
using CarsRent.WPF.Pages.MainFramePages;
using System.Windows;
using CarsRent.LIB.DataBase;
using System;

namespace CarsRent.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartupDataBaseConnection();
        }

        private void StartupDataBaseConnection()
        {
            var s = new ApplicationContext();
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