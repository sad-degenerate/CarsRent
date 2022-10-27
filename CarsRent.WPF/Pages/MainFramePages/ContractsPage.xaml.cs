using CarsRent.LIB.Model;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class ContractsPage : Page
    {
        private readonly ContractsPageController _controller;

        public ContractsPage()
        {
            InitializeComponent();

            _controller = new ContractsPageController();
            
            UpdateDataGrid();
        }
        
        public async void UpdateDataGrid()
        {
            DgContracts.ItemsSource = await _controller.GetDataGridItems<Contract>
                (TbxSearch.Text, _controller.GetSkipCount(), _controller.PageSize).AsTask();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            DeleteContract(contract);
        }

        private void DeleteContract(Contract contract)
        {
            _controller.DeleteEntity(contract);
            
            UpdateDataGrid();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddContractPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            NavigationService.Navigate(new AddContractPage(contract));
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            if (DgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            _controller.OpenDocumentFolder(contract);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (DgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            _controller.Print(contract);
        }

        private void btnGoto_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TbxPageNumber.Text, out var pageNumber) == false)
            {
                return;
            }

            GoTo(pageNumber);
        }

        private void btnPageRight_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_controller.CurrentPage + 1);
        }

        private void btnPageLeft_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_controller.CurrentPage - 1);
        }

        private void GoTo(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                return;
            }

            _controller.CurrentPage = pageNumber;
            TbxPageNumber.Text = pageNumber.ToString();
            
            UpdateDataGrid();
        }
    }
}