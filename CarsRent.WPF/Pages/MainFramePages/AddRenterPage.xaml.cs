using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;
using CarsRent.LIB.Model;
using CarsRent.WPF.ViewControllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddRenterPage : Page
    {
        private readonly AddRenterPageController _addEditController;
        private readonly FillingRenterFieldsController _fillingFieldsController;
        
        public AddRenterPage(Human? renter = null)
        {
            InitializeComponent();

            _addEditController = new AddRenterPageController(renter);
            _fillingFieldsController = new FillingRenterFieldsController();

            if (renter == null)
            {
                return;
            }

            var collection = new UIElementCollection(Panel, this);
            var valuesRelDict = _fillingFieldsController.CreateValuesRelationDict(renter);
            _fillingFieldsController.FillFields(ref collection, valuesRelDict);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AddEditRenter();
        }

        private void AddEditRenter()
        {
            BtnSave.IsEnabled = false;
            
            var collection = Panel.Children;
            var valuesRelDict = new Dictionary<string, string>(_fillingFieldsController.CreateValuesRelationDict(collection));
            var error = _addEditController.AddEditEntity(collection, valuesRelDict);

            if (string.IsNullOrWhiteSpace(error) == false)
            {
                MessageBox.Show(error, "Не удалось добавить арендатора.");
            
                BtnSave.IsEnabled = true;
            }
            else
            {
                NavigationService.GoBack();
            }
        }
    }
}