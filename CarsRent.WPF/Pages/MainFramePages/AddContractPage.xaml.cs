using CarsRent.LIB.Model;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddContractPage : Page
    {
        private readonly AddContractPageController _controller;

        public AddContractPage(Contract? contract = null)
        {
            InitializeComponent();

            _controller = new AddContractPageController(contract);
            _controller.CreateComboBoxesValues(ref CbxRideType);

            UpdateCarsList();
            UpdateRentersList();

            if (contract == null)
            {
                return;
            }

            var collection = new UIElementCollection(Panel, this);
            _controller.FillFields(ref collection);
        }

        private async void UpdateCarsList()
        {
            await _controller.UpdateCarsItemsSourceAsync(TbxSearchCar.Text, 0, 3, ref LbxCar);
        }

        private async void UpdateRentersList()
        {
            await _controller.UpdateRentersItemsSourceAsync(TbxSearchRenter.Text, 0, 3, ref LbxRenter);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AddEditContract();
        }

        private async void AddEditContract()
        {
            BtnSave.IsEnabled = false;
            
            var collection = new UIElementCollection(Panel, this);
            var error = await _controller.AddEditEntityAsync(collection);

            if (string.IsNullOrWhiteSpace(error))
            {
                NavigationService.GoBack();
            }
            
            MessageBox.Show(error, "Не удалось добавить договор.");
            
            BtnSave.IsEnabled = true;
        }

        private void tbxSearchRenter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRentersList();
        }

        private void tbxSearchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCarsList();
        }
    }
}