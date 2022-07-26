using CarsRent.WPF.Pages.Settings;
using CarsRent.WPF.Pages.MainFramePages;
using System.Windows;
using CarsRent.LIB.Model;
using CarsRent.LIB.DataBase;

namespace CarsRent.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // первое соединение с базой данных происходит долго,
            // в начале приложения проще это оставить
            var load = Commands<Car>.SelectById(1);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow();
            window.Owner = this;
            window.Show();
        }

        private void btnRenters_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Renters();
        }

        private void mainFrame_ContentRendered(object sender, System.EventArgs e)
        {
            mainFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }

        private void btnCars_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Cars();
        }

        private void btnContracts_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Contracts();
        }
    }
}