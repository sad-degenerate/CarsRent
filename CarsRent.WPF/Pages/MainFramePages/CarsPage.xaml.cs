using System.Windows;
using CarsRent.LIB.Model;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class CarsPage : Page
    {
        private readonly CarsPageController _controller;

        public CarsPage()
        {
            InitializeComponent();

            _controller = new CarsPageController();
            
            UpdateDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgCars.SelectedItem is not Car car)
            {
                return;
            }

            DeleteCar(car);
        }

        private async void DeleteCar(Car car)
        {
            _controller.DeleteEntity(car);

            MessageBox.Show("Автомобиль удален.", "Удаление.");
            
            UpdateDataGrid();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgCars.SelectedItem is not Car car)
            {
                return;
            }

            NavigationService.Navigate(new AddCarPage(car));
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private async void UpdateDataGrid()
        {
            DgCars.ItemsSource = await _controller.GetDataGridItems<Car>
                (TbxSearch.Text, _controller.GetSkipCount(), _controller.PageSize).AsTask();
        }

        private void btnPageLeft_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_controller.CurrentPage - 1);
        }

        private void btnPageRight_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_controller.CurrentPage + 1);
        }

        private void btnGoto_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbxPageNumber.Text, out var pageNumber) == false)
            {
                return;
            }

            GoTo(pageNumber);
        }

        private void GoTo(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return;
            }

            _controller.CurrentPage = pageNumber;
            TbxPageNumber.Text = _controller.CurrentPage.ToString();
            
            UpdateDataGrid();
        }
    }
}