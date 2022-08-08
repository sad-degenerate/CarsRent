using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Cars : Page
    {
        private List<Car> _cars;
        private int _currentPage = 1;
        private int _pageSize = 10;

        public Cars()
        {
            InitializeComponent();

            var serializator = new SettingsSerializator<DisplaySettings>();
            var settings = serializator.Deserialize();

            if (settings != null)
                _pageSize = settings.TableOnePageElementsCount;

            tbxPageNumber.Text = _currentPage.ToString();
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var car = dgCars.SelectedItem as Car;

            if (car == null)
                return;

            Commands<Car>.Delete(car);

            UpdateDataGrid();
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarPage());
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var car = dgCars.SelectedItem as Car;

            if (car == null)
                return;

            NavigationService.Navigate(new AddCarPage(car));
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            int startIndex;
            if (_currentPage == 1)
                startIndex = _currentPage;
            else
                startIndex = _currentPage * _pageSize;

            if (tbxSearch.Text == "")
                _cars = Commands<Car>.SelectGroup(startIndex, _pageSize).ToList();
            else
            {
                _cars = FindCar(tbxSearch.Text);
                _cars = _cars.Skip(startIndex).Take(_pageSize).ToList();
            }

            dgCars.ItemsSource = _cars;
            tbxPageNumber.Text = _currentPage.ToString();
        }

        private List<Car> FindCar(string text)
        {
            var cars = Commands<Car>.SelectAll();
            var carsResult = new List<Car>();
            var words = text.Split(' ');

            foreach (var car in cars)
            {
                var carText = car.ToString();

                var addToResult = true;
                foreach (var word in words)
                {
                    if (carText.Contains(word) == false)
                    {
                        addToResult = false;
                        break;
                    }
                }

                if (addToResult == true)
                    carsResult.Add(car);
            }

            return carsResult;
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnPageLeft_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_currentPage > 0)
                _currentPage--;

            UpdateDataGrid();
        }

        private void btnPageRight_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _currentPage++;
            UpdateDataGrid();
        }

        private void btnGoto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (int.TryParse(tbxPageNumber.Text, out int pageNumber) == false)
                return;

            _currentPage = pageNumber;
            UpdateDataGrid();
        }
    }
}