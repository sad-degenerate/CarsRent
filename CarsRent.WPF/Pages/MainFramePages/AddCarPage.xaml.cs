using CarsRent.LIB.Model;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddCarPage : Page
    {
        private readonly AddCarPageController _controller;

        public AddCarPage(Car car = null)
        {
            InitializeComponent();
            
            _controller = new AddCarPageController(car);
            _controller.CreateComboBoxesValues(ref CbxWheelsType, ref CbxStatus);

            UpdateItemsSource();

            if (_controller.Car == null)
            {
                return;
            }
            
            var collection = new UIElementCollection(Panel, this);
            _controller.FillFields(ref collection);
            _controller.SelectListBoxSelectedOwner(ref LbxOwner);
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddEditCar();
        }

        private async void AddEditCar()
        {
            BtnSave.IsEnabled = false;
            
            var collection = new UIElementCollection(Panel, this);
            var error = await _controller.AddEditCarAsync(collection);

            if (string.IsNullOrWhiteSpace(error))
            {
                NavigationService.GoBack();
            }
            
            LblError.Content = error;
            
            BtnSave.IsEnabled = true;
        }

        private async void UpdateItemsSource()
        {
            LbxOwner.ItemsSource = await _controller.GetOwnersAsync(TbxSearchOwner.Text, 0, 3);
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateItemsSource();
        }
    }
}