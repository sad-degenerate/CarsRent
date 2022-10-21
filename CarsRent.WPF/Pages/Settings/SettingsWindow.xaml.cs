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
            if (sender is not Button btn)
            {
                return;
            }
            
            EnableAllButtonsButPressed(btn);

            Page? page = btn.Name switch
            {
                "btnDataSettings" => new TemplatesSettingsPage(),
                "btnDisplaySettings" => new DisplaySettingsPage(),
                "btnLandlordSettings" => new OwnerSettingsPage(),
                _ => null
            };

            if (page != null)
            {
                settingsFrame.Navigate(page);
            }   
        }

        private void EnableAllButtonsButPressed(UIElement pressedButton)
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