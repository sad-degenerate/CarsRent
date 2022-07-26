using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Cars : Page
    {
        private List<Car> _cars;

        public Cars()
        {
            InitializeComponent();
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
            if (tbxSearch.Text == "")
                _cars = Commands<Car>.SelectAll().ToList();
            else
                _cars = FindCar(tbxSearch.Text);

            dgCars.ItemsSource = _cars;
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
    }
}