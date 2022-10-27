using System.Linq;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class CarsPage : Page
    {
        private int _currentPage = 1;
        private readonly int _pageSize;

        public CarsPage()
        {
            InitializeComponent();

            var serializator = new SettingsSerializator<DisplaySettings>();
            var settings = serializator.Deserialize();
            _pageSize = settings.TableOnePageElementsCount;
        }

        private void UpdateCurrentPage()
        {
            TbxPageNumber.Text = _currentPage.ToString();
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DgCars.SelectedItem is not Car car)
            {
                return;
            }

            DeleteCar(car);

            UpdateDataGrid();
        }

        private static void DeleteCar(Car car)
        {
            BaseCommands<Car>.DeleteAsync(car);
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCarPage());
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
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

            DgCars.ItemsSource = TbxSearch.Text == string.Empty
                ? BaseCommands<Car>.SelectGroupAsync(skipCount, _pageSize).ToList()
                : BaseCommands<Car>.FindAndSelectAsync(TbxSearch.Text, 0, _pageSize).ToList();

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
            if (int.TryParse(TbxPageNumber.Text, out int pageNumber) == false)
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