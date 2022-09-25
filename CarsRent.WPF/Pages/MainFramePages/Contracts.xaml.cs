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

        private void UpdateDataGrid()
        {
            int startIndex;
            if (_currentPage == 1)
            {
                startIndex = _currentPage;
            }
            else
            {
                startIndex = _currentPage * _pageSize;
            }

            if (tbxSearch.Text == "")
            {
                _contracts = Commands<ContractDetails>.SelectGroup(startIndex, _pageSize).ToList();
            }
            else
            {
                _contracts = Commands<ContractDetails>.FindAndSelect(tbxSearch.Text, startIndex, _pageSize).ToList();
            }

            foreach (var contract in _contracts)
            {
                try
                {
                    Commands<Human>.SelectById((int)contract.RenterId);
                    Commands<Car>.SelectById((int)contract.CarId);
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"В договоре {contract.Id} отсутствует связь с другим объектом базы данных." +
                        $" Попробуйте пересоздать договор");
                }
            }

            dgContracts.ItemsSource = _contracts;
            UpdateCurrentPage();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgContracts.SelectedItem is not ContractDetails contract)
            {
                return;
            }

            Commands<ContractDetails>.Delete(contract);

            UpdateDataGrid();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddContractPage());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgContracts.SelectedItem is not ContractDetails contract)
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
            if (dgContracts.SelectedItem is not ContractDetails contract)
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
                MessageBox.Show("Не удалось открыть директорию. Попробуйте заново сохранить договор.");
            }
        }

        private string GetDocumentFolder(ContractDetails contract)
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();

            var settings = settingsSerializator.Deserialize();
            if (settings == null)
            {
                throw new Exception("Введите в настройках первичные данные.");
            }

            return Path.Combine(settings.OutputFolder, $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (dgContracts.SelectedItem is not ContractDetails contract)
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

            var documentName = $"{contract.ConclusionDate} {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";
            var filesPath = Path.Combine(outputFolder, documentName);

            ContractPrinter.Print($"{filesPath} договор.docx", 2);
            ContractPrinter.Print($"{filesPath} акт.docx", 2);
            ContractPrinter.Print($"{filesPath} уведомление.docx", 1);
        }

        private void btnGoto_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbxPageNumber.Text, out int pageNumber) == false)
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