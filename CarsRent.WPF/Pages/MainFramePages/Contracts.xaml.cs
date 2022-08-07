using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using CarsRent.LIB.Word;
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
        private List<ContractDetails> _contractDetails;

        public Contracts()
        {
            InitializeComponent();
        }

        private void UpdateDataGrid()
        {
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

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var contract = dgContracts.SelectedItem as ContractDetails;

            if (contract == null)
                return;

            var dir = GetDocumentFolder(contract);

            if (Directory.Exists(dir))
                Process.Start("explorer.exe", dir);
            else
                MessageBox.Show("Не удалось открыть директорию. Попробуйте заново сохранить договор.");
        }

        private string GetDocumentFolder(ContractDetails contract)
        {
            var settingsSerializator = new SettingsSerializator<TemplatesSettings>();
            var settings = settingsSerializator.Deserialize();
            return Path.Combine(settings.OutputFolder, $"{contract.Car.Color} {contract.Car.Brand} {contract.Car.Model}");
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var contract = dgContracts.SelectedItem as ContractDetails;

            if (contract == null)
                return;

            var outputFolder = GetDocumentFolder(contract);
            var documentName = $"{contract.ConclusionDate} {contract.Renter.Surname} {contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}.";

            var filesPath = Path.Combine(outputFolder, documentName);

            ContractPrinter.Print($"{filesPath} договор.docx", 2);
            ContractPrinter.Print($"{filesPath} акт.docx", 2);
            ContractPrinter.Print($"{filesPath} уведомление.docx", 1);
        }
    }
}