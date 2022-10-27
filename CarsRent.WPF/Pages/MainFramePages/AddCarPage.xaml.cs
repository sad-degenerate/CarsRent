using System.Windows;
using CarsRent.LIB.Model;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddCarPage : Page
    {
        private readonly AddCarPageController _controller;

        public AddCarPage(Car? car = null)
        {
            InitializeComponent();
            
            _controller = new AddCarPageController(car);
            
            _controller.CreateComboBoxesValues(ref CbxWheelsType, ref CbxStatus);
            UpdateItemsSource();

            if (car == null)
            {
                return;
            }
            
            var collection = new UIElementCollection(Panel, this);
            _controller.FillFields(ref collection);
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddEditCar();
        }

        private async void AddEditCar()
        {
            BtnSave.IsEnabled = false;

            var collection = new UIElementCollection(Panel, this);
            var error = await _controller.AddEditEntityAsync(collection);

            if (string.IsNullOrWhiteSpace(error))
            {
                NavigationService.GoBack();
            }
            
            MessageBox.Show(error, "Не удалось добавить автомобиль.");
            
            BtnSave.IsEnabled = true;
        }

        private async void UpdateItemsSource()
        {
            await _controller.UpdateOwnersItemsSourceAsync(TbxSearchOwner.Text, 0, 3, ref LbxOwner);
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateItemsSource();
        }
    }
}