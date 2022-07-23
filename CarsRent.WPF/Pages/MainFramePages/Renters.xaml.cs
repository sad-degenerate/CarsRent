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
        public Renters()
        {
            InitializeComponent();

            var renters = Commands<Human>.Select(1, 1).ToList();
            
            AddRentersToView(renters);
        }

        private void AddRentersToView(List<Human> renters)
        {
            dgRenters.ItemsSource = renters;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRenterPage());
        }
    }
}