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
        private int _currentPage = 1;
        private readonly int _pageSize;

        public Contracts()
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

        public void UpdateDataGrid()
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

            dgContracts.ItemsSource = tbxSearch.Text == string.Empty
                ? Commands<Contract>.SelectGroup(skipCount, _pageSize).ToList()
                : Commands<Contract>.FindAndSelect(tbxSearch.Text, 0, _pageSize).ToList();

            UpdateCurrentPage();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            DeleteContract(contract);

            UpdateDataGrid();
        }

        private static void DeleteContract(Contract contract)
        {
            Commands<Contract>.Delete(contract);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddContractPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

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
            if (dgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            string? dir;
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
            {
                Process.Start("explorer.exe", dir);
            }
            else
            {
                MessageBox.Show("Не удалось открыть директорию. Попробуйте сохранить договор.");
            }
        }

        private string GetDocumentFolder(Contract contract)
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var settings = settingsSerializator.Deserialize();
            
            if (settings == null)
            {
                throw new Exception("Введите в настройках первичные данные.");
            }

            return Path.Combine(settings.OutputFolder, 
                $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (dgContracts.SelectedItem is not Contract contract)
            {
                return;
            }

            string? outputFolder;
            try
            {
                outputFolder = GetDocumentFolder(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                return;
            }

            var documentName = $"{contract.ConclusionDate} {contract.Renter.Human.Surname} " +
                               $"{contract.Renter.Human.Name[0]}.{contract.Renter.Human.Patronymic[0]}.";
            var filesPath = Path.Combine(outputFolder, documentName);

            ContractPrinter.Print($"{filesPath} договор.docx", 2);
            ContractPrinter.Print($"{filesPath} акт.docx", 2);
            ContractPrinter.Print($"{filesPath} уведомление.docx", 1);
        }

        private void btnGoto_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbxPageNumber.Text, out var pageNumber) == false)
            {
                return;
            }

            GoTo(pageNumber);
        }

        private void btnPageRight_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage + 1);
        }

        private void btnPageLeft_Click(object sender, RoutedEventArgs e)
        {
            GoTo(_currentPage - 1);
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