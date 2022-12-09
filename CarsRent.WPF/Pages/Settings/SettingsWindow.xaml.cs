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

        private void EnableAllButtonsButPressed(UIElement pressedButton)
        {
            var buttons = SettingsWindowGrid.Children.OfType<Button>();
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }

            pressedButton.IsEnabled = false;
        }
        
        private void OnSettingsCategoryButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn)
            {
                return;
            }
            
            EnableAllButtonsButPressed(btn);

            Page? page = btn.Name switch
            {
                "TemplatesSettings" => new TemplatesSettingsPage(),
                "DisplaySettings" => new DisplaySettingsPage(),
                "OwnersSettings" => new OwnerSettingsPage(),
                "PrintSettings" => new PrintSettingsPage(),
                _ => null
            };

            if (page != null)
            {
                SettingsFrame.Navigate(page);
            }   
        }

        private void settingsFrame_ContentRendered(object sender, EventArgs e)
        {
            SettingsFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }
    }
}