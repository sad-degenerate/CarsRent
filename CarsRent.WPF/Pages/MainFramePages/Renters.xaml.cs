using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Renters : Page
    {
        private List<Human> _renters;
        private int _currentPage = 1;
        private int _pageSize = 10;

        public Renters()
        {
            InitializeComponent();

            var serializator = new SettingsSerializator<DisplaySettings>();
            var settings = serializator.Deserialize();

            if (settings != null)
                _pageSize = settings.TableOnePageElementsCount;

            tbxPageNumber.Text = _currentPage.ToString();
        }

        private void UpdateDataGrid()
        {
            int startIndex;
            if (_currentPage == 1)
                startIndex = _currentPage;
            else
                startIndex = _currentPage * _pageSize;

            if (tbxSearch.Text == "")
                _renters = Commands<Human>.SelectGroup(startIndex, _pageSize).ToList();
            else
            {
                _renters = FindHuman(tbxSearch.Text);
                _renters = _renters.Skip(startIndex).Take(_pageSize).ToList();
            } 

            dgRenters.ItemsSource = _renters;
            tbxPageNumber.Text = _currentPage.ToString();
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

        private void btnPageLeft_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage - 1);
            UpdateDataGrid();
        }

        private void btnPageRight_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage + 1);
            UpdateDataGrid();
        }

        private void btnGoto_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbxPageNumber.Text, out int pageNumber) == false)
                return;

            GoTo(pageNumber);
            UpdateDataGrid();
        }

        private void GoTo(int pageNumber)
        {
            if (pageNumber <= 0)
                return;

            _currentPage = pageNumber;
            UpdateDataGrid();
        }
    }
}