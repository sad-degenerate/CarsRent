using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using CarsRent.LIB.Word;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Contracts : Page
    {
        private List<ContractDetails> _contracts;
        private int _currentPage = 1;
        private int _pageSize = 10;

        public Contracts()
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
                _contracts = Commands<ContractDetails>.SelectGroup(startIndex, _pageSize).ToList();
            else
            {
                _contracts = FindContract(tbxSearch.Text);
                _contracts = _contracts.Skip(startIndex).Take(_pageSize).ToList();
            }

            dgContracts.ItemsSource = _contracts;
            tbxPageNumber.Text = _currentPage.ToString();
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

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var contract = dgContracts.SelectedItem as ContractDetails;

            if (contract == null)
                return;

            var dir = string.Empty;

            try
            {
                dir = GetDocumentFolder(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }
            

            if (Directory.Exists(dir))
                Process.Start("explorer.exe", dir);
            else
                MessageBox.Show("Не удалось открыть директорию. Попробуйте заново сохранить договор.");
        }

        private string GetDocumentFolder(ContractDetails contract)
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();

            var settings = settingsSerializator.Deserialize();
            if (settings == null)
                throw new Exception("Введите в настройках первичные данные.");

            return Path.Combine(settings.OutputFolder, $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var contract = dgContracts.SelectedItem as ContractDetails;

            if (contract == null)
                return;

            var outputFolder = string.Empty;
            try
            {
                outputFolder = GetDocumentFolder(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }

            var documentName = $"{contract.ConclusionDate} {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";

            var filesPath = Path.Combine(outputFolder, documentName);

            ContractPrinter.Print($"{filesPath} договор.docx", 2);
            ContractPrinter.Print($"{filesPath} акт.docx", 2);
            ContractPrinter.Print($"{filesPath} уведомление.docx", 1);
        }

        private void btnGoto_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbxPageNumber.Text, out int pageNumber) == false)
                return;

            GoTo(pageNumber);
            UpdateDataGrid();
        }

        private void btnPageRight_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage + 1);
            UpdateDataGrid();
        }

        private void btnPageLeft_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage - 1);
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