using CarsRent.WPF.Pages.Settings;
using CarsRent.WPF.Pages.MainFramePages;
using System.Windows;
using CarsRent.LIB.DataBase;

namespace CarsRent.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var context = ApplicationContext.Instance();
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
    }
}