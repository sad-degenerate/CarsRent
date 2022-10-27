using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;
using CarsRent.LIB.Model;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddRenterPage : Page
    {
        private readonly AddRenterPageController _controller;
        
        public AddRenterPage(Human? renter = null)
        {
            InitializeComponent();

            _controller = new AddRenterPageController(renter);

            if (renter == null)
            {
                return;
            }
            
            var collection = new UIElementCollection(Panel, this);
            _controller.FillFields(ref collection);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            AddEditRenter();
        }

        private async void AddEditRenter()
        {
            BtnSave.IsEnabled = false;
            
            var collection = new UIElementCollection(Panel, this);
            var error = await _controller.AddEditEntityAsync(collection);

            if (string.IsNullOrWhiteSpace(error))
            {
                NavigationService.GoBack();
            }
            
            MessageBox.Show(error, "Не удалось добавить арендатора.");
            
            BtnSave.IsEnabled = true;
        }
    }
}