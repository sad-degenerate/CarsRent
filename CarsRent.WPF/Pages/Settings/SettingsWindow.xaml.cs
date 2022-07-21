using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // TODO: Прятать или делать неактивным главное окно, когда открыто окно настроек
            //this.Owner.Hide();
        }

        private void btnDataSettings_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            EnableAllButtonsButPressed(btn);

            Page frame;

            switch (btn.Name)
            {
                case "btnDataSettings":
                    frame = new DataSettings();
                    break;

                default:
                    frame = null;
                    break;
            }

            if (frame != null)
                settingsFrame.Navigate(new DataSettings());
        }

        private void EnableAllButtonsButPressed(Button pressedButton)
        {
            var buttons = settingsWindowGrid.Children.OfType<Button>();
            foreach (var button in buttons)
                if (button.Tag.ToString() == "settingsType")
                    button.IsEnabled = true;

            pressedButton.IsEnabled = false;
        }

        private void settingsFrame_ContentRendered(object sender, System.EventArgs e)
        {
            settingsFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}