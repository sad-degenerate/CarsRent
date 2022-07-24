using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using System;
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
            // TODO: Множественное удаление
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
            var resultRenters = new List<Human>();

            foreach (var renter in renters)
            {
                var fullName = $"{renter.Surname} {renter.Name} {renter.Patronymic}";

                if (fullName.Contains(text))
                    resultRenters.Add(renter);
                else if (renter.BirthDate.Contains(text))
                    resultRenters.Add(renter);
                else if (renter.PhoneNumber.Contains(text))
                    resultRenters.Add(renter);
                else if (renter.IdentityNumber.Contains(text))
                    resultRenters.Add(renter);
                else if (renter.IssuingDate.Contains(text))
                    resultRenters.Add(renter);
                else if (renter.IssuingOrganization.Contains(text))
                    resultRenters.Add(renter);
                else if (renter.RegistrationPlace.Contains(text))
                    resultRenters.Add(renter);
            }

            return resultRenters;
        }
    }
}