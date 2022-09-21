using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void SettingsType_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            EnableAllButtonsButPressed(btn);

            Page page;
            switch (btn.Name)
            {
                case "btnDataSettings":
                    page = new DataSettings();
                    break;

                case "btnDisplaySettings":
                    page = new DisplaySettingsPage();
                    break;

                default:
                    page = null;
                    break;
            }

            if (page != null)
            {
                settingsFrame.Navigate(page);
            }   
        }

        private void EnableAllButtonsButPressed(Button pressedButton)
        {
            var buttons = settingsWindowGrid.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                if (button.Tag.ToString() == "settingsType")
                {
                    button.IsEnabled = true;
                }
            }

            pressedButton.IsEnabled = false;
        }

        private void settingsFrame_ContentRendered(object sender, EventArgs e)
        {
            settingsFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}