using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Renters : Page
    {
        private int _currentPage = 1;
        private readonly int _pageSize;

        public Renters()
        {
            InitializeComponent();

            var serializator = new SettingsSerializator<DisplaySettings>();
            var settings = serializator.Deserialize();
            _pageSize = settings.TableOnePageElementsCount;
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

            var renters = string.IsNullOrWhiteSpace(TbxSearch.Text)
                ? BaseCommands<Renter>.SelectGroupAsync(skipCount, _pageSize).ToList()
                : BaseCommands<Renter>.FindAndSelectAsync(TbxSearch.Text, 0, _pageSize).ToList();

            DgRenters.ItemsSource = SelectHumansFromRenters(renters);

            UpdateCurrentPage();
        }

        private static List<Human?> SelectHumansFromRenters(List<Renter?> renters)
        {
            return renters.Select(renter => BaseCommands<Human>.SelectByIdAsync(renter.HumanId)).ToList();
        }

        private void UpdateCurrentPage()
        {
            TbxPageNumber.Text = _currentPage.ToString();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgRenters.SelectedItem is not Human renter)
            {
                return;
            }

            DeleteRenter(renter);

            UpdateDataGrid();
        }

        private static void DeleteRenter(Human human)
        {
            foreach (var renter in human.Renters)
            {
                BaseCommands<Renter>.DeleteAsync(renter);
            }
            
            BaseCommands<Human>.DeleteAsync(human);
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnPageLeft_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage - 1);
        }

        private void btnPageRight_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage + 1);
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

            _currentPage = pageNumber;
            UpdateDataGrid();
        }
    }
}