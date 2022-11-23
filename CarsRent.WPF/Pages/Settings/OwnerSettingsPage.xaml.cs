using CarsRent.LIB.Settings;
using System.Collections.Generic;
using System.Windows.Controls;
using CarsRent.WPF.ViewControllers;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class OwnerSettingsPage : Page
    {
        public OwnerSettingsPage()
        {
            InitializeComponent();
            UpdateOwners();
        }

        private async void UpdateOwners()
        {
            var list = await OwnersSettings.GetOwners(TbxSearchOwner.Text, 0, 10);
            LbxOwner.ItemsSource = list;
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOwners();
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var controller = new FillingCarFieldsController();
            var fields = new Dictionary<string, string>(controller.CreateValuesRelationDict(Panel.Children));

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