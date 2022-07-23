using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Renters : Page
    {
        private List<Human> _renters;
        public Renters()
        {
            InitializeComponent();

            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            _renters = Commands<Human>.Select(1, 10).ToList();
            dgRenters.ItemsSource = _renters;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRenterPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var renters = dgRenters.SelectedItems as List<Human>;
            
            if (renters == null)
                return;

            NavigationService.Navigate(new AddRenterPage(renters.First()));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var renters = dgRenters.SelectedItems as List<Human>;
            
            if (renters == null)
                return;

            foreach (var renter in renters)
                Commands<Human>.Delete(renter);
        }
    }
}