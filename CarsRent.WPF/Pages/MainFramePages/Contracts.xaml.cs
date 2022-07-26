using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Contracts : Page
    {
        private List<ContractDetails> _contractDetails;

        public Contracts()
        {
            InitializeComponent();
        }

        private void UpdateDataGrid()
        {
            // TODO: одновременно не работают: вывод русской версии типа поездки и вывод ФИО арендатора

            if (tbxSearch.Text == "")
                _contractDetails = Commands<ContractDetails>.SelectAll().ToList();
            else
                _contractDetails = FindContract(tbxSearch.Text);

            FillDataGrid();
        }

        private void FillDataGrid()
        {
            dgContracts.ItemsSource = _contractDetails;
        }

        private List<ContractDetails> FindContract(string text)
        {
            var contracts = Commands<ContractDetails>.SelectAll();
            var contractResult = new List<ContractDetails>();
            var words = text.Split(' ');

            foreach (var contract in contracts)
            {
                var contractText = contract.ToString();

                var addToResult = true;
                foreach (var word in words)
                {
                    if (contractText.Contains(word) == false)
                    {
                        addToResult = false;
                        break;
                    }
                }

                if (addToResult == true)
                    contractResult.Add(contract);
            }

            return contractResult;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var contract = dgContracts.SelectedItem as ContractDetails;

            if (contract == null)
                return;

            Commands<ContractDetails>.Delete(contract);

            UpdateDataGrid();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddContractPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var contract = dgContracts.SelectedItem as ContractDetails;

            if (contract == null)
                return;

            NavigationService.Navigate(new AddContractPage(contract));
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}