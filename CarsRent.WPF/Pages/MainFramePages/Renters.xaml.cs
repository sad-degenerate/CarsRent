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
            if (tbxSearch.Text == "")
                _renters = Commands<Human>.SelectAll().ToList();
            else
                _renters = FindHuman(tbxSearch.Text);

            dgRenters.ItemsSource = _renters;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRenterPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var renter = dgRenters.SelectedItem as Human;
            
            if (renter == null)
                return;

            NavigationService.Navigate(new AddRenterPage(renter));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var renter = dgRenters.SelectedItem as Human;
            
            if (renter == null)
                return;

            Commands<Human>.Delete(renter);

            UpdateDataGrid();
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private List<Human> FindHuman(string text)
        {
            var renters = Commands<Human>.SelectAll();
            var renterResult = new List<Human>();
            var words = text.Split(' ');

            foreach (var renter in renters)
            {
                var renterText = renter.ToString();

                var addToResult = true;
                foreach (var word in words)
                {
                    if (renterText.Contains(word) == false)
                    {
                        addToResult = false;
                        break;
                    }
                }

                if (addToResult == true)
                    renterResult.Add(renter);
            }

            return renterResult;
        }
    }
}