using System.Linq;
using CarsRent.LIB.Model;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class RentersPage : Page
    {
        private readonly RentersPageController _controller;

        public RentersPage()
        {
            InitializeComponent();

            _controller = new RentersPageController();
            
            UpdateDataGrid();
        }

        private async void UpdateDataGrid()
        {
            var renters = await _controller.GetDataGridItemsAsync<Renter>
                (TbxSearch.Text, _controller.GetSkipCount(), _controller.PageSize);
            DgRenters.ItemsSource = from renter in renters select renter.Human;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRenterPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgRenters.SelectedItem is not Human renter)
            {
                return;
            }

            NavigationService.Navigate(new AddRenterPage(renter));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgRenters.SelectedItem is not Human renter)
            {
                return;
            }

            DeleteRenter(renter);
        }

        private void DeleteRenter(Human renter)
        {
            _controller.DeleteEntity(renter);
            
            UpdateDataGrid();
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
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
            TbxPageNumber.Text = pageNumber.ToString();
            
            UpdateDataGrid();
        }

        private void RentersPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}