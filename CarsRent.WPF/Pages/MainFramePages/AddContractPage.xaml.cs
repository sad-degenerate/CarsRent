using System.Collections.Generic;
using CarsRent.LIB.Model;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;
using CarsRent.WPF.ViewControllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddContractPage : Page
    {
        private readonly AddContractPageController _addEditController;
        private readonly FillingContractFieldsController _fillingFieldsController;

        public AddContractPage(Contract? contract = null)
        {
            InitializeComponent();

            _addEditController = new AddContractPageController(contract);
            _fillingFieldsController = new FillingContractFieldsController();
            _addEditController.CreateComboBoxesValues(ref CbxRideType);

            UpdateCarsList();
            UpdateRentersList();

            if (contract == null)
            {
                return;
            }

            var collection = new UIElementCollection(Panel, this);
            var valuesRelDict = _fillingFieldsController.CreateValuesRelationDict(contract);
            _fillingFieldsController.FillFields(ref collection, valuesRelDict);
        }

        private async void UpdateCarsList()
        {
            await _addEditController.UpdateCarsItemsSourceAsync(TbxSearchCar.Text, 0, 3, ref LbxCar);
        }

        private async void UpdateRentersList()
        {
            await _addEditController.UpdateRentersItemsSourceAsync(TbxSearchRenter.Text, 0, 3, ref LbxRenter);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AddEditContract();
        }

        private async void AddEditContract()
        {
            BtnSave.IsEnabled = false;
            
            var collection = Panel.Children;
            var valuesRelDict = new Dictionary<string, string>(_fillingFieldsController.CreateValuesRelationDict(collection));
            var error = _addEditController.AddEditEntity(collection, valuesRelDict);

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