using System.Linq;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Cars : Page
    {
        private int _currentPage = 1;
        private readonly int _pageSize;

        public Cars()
        {
            InitializeComponent();

            var serializator = new SettingsSerializator<DisplaySettings>();
            var settings = serializator.Deserialize();
            _pageSize = settings.TableOnePageElementsCount;
        }

        private void UpdateCurrentPage()
        {
            tbxPageNumber.Text = _currentPage.ToString();
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dgCars.SelectedItem is not Car car)
            {
                return;
            }

            DeleteCar(car);

            UpdateDataGrid();
        }

        private static void DeleteCar(Car car)
        {
            Commands<Car>.Delete(car);
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarPage());
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dgCars.SelectedItem is not Car car)
            {
                return;
            }

            NavigationService.Navigate(new AddCarPage(car));
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            int skipCount;
            if (_currentPage == 1)
            {
                skipCount = 0;
            }
            else
            {
                skipCount = _currentPage * (_pageSize - 1);
            }

            dgCars.ItemsSource = tbxSearch.Text == string.Empty
                ? Commands<Car>.SelectGroup(skipCount, _pageSize).ToList()
                : Commands<Car>.FindAndSelect(tbxSearch.Text, 0, _pageSize).ToList();

            UpdateCurrentPage();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnPageLeft_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GoTo(_currentPage - 1);
        }

        private void btnPageRight_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GoTo(_currentPage + 1);
        }

        private void btnGoto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (int.TryParse(tbxPageNumber.Text, out int pageNumber) == false)
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

            _currentPage = pageNumber;
            UpdateDataGrid();
        }
    }
}