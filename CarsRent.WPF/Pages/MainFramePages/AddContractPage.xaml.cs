using System.Collections.Generic;
using System.Threading.Tasks;
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

            var collection = Panel.Children;
            var valuesRelDict = _fillingFieldsController.CreateValuesRelationDict(contract);
            _fillingFieldsController.FillFields(ref collection, valuesRelDict);
        }

        private async void UpdateCarsList()
        {
            LbxCar.ItemsSource = await _addEditController.UpdateCarsItemsSourceAsync(TbxSearchCar.Text, 0, 3);

            var selectedItemId = _addEditController.GetSelectedCarId();
            if (selectedItemId.HasValue == false)
            {
                return;
            }

            foreach (var item in LbxCar.ItemsSource)
            {
                if (item is not Car car || car.Id != selectedItemId)
                {
                    continue;
                }

                LbxCar.SelectedItem = item;
            }
        }

        private async void UpdateRentersList()
        {
            LbxRenter.ItemsSource = await _addEditController.UpdateRentersItemsSourceAsync(TbxSearchRenter.Text, 0, 3);

            var selectedItemId = _addEditController.GetSelectedRenterId();
            if (selectedItemId.HasValue == false)
            {
                return;
            }

            foreach (var item in LbxRenter.ItemsSource)
            {
                if (item is not Human human || human.Id != selectedItemId)
                {
                    continue;
                }

                LbxCar.SelectedItem = item;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AddEditContract();
        }

        private void AddEditContract()
        {
            BtnSave.IsEnabled = false;
            
            var collection = Panel.Children;
            var valuesRelDict = new Dictionary<string, string>(_fillingFieldsController.CreateValuesRelationDict(collection));
            var error = _addEditController.AddEditEntity(collection, valuesRelDict);

            if (string.IsNullOrWhiteSpace(error) == false)
            {
                MessageBox.Show(error, "Не удалось добавить договор.");
            
                BtnSave.IsEnabled = true;
            }
            else
            {
                NavigationService.GoBack();
            }
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