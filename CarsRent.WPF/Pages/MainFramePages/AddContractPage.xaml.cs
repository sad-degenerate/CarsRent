using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using CarsRent.LIB.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddContractPage : Page
    {
        private ContractDetails _contractDetails;
        private readonly Dictionary<string, RideType> _rideType;

        public AddContractPage(ContractDetails contractDetails = null)
        {
            InitializeComponent();

            _rideType = new Dictionary<string, RideType>
            {
                { "по городу", RideType.InTheCity },
                { "за городом", RideType.OutsideTheCity }
            };

            cbxRideType.ItemsSource = _rideType.Keys;
            lbxCar.ItemsSource = Commands<Car>.SelectAll().ToList();
            lbxRenter.ItemsSource = Commands<Human>.SelectAll().ToList();

            if (contractDetails == null)
                return;

            FillField(contractDetails);
            _contractDetails = contractDetails;
        }

        private void FillField(ContractDetails contractDetails)
        {
            tbxDeposit.Text = contractDetails.Deposit.ToString();
            tbxPrice.Text = contractDetails.Price.ToString();
            tbxConclusionDate.Text = contractDetails.ConclusionDate.ToString("dd.MM.yyyy");
            tbxEndDate.Text = contractDetails.EndDate.ToString("dd.MM.yyyy");
            tbxEndTime.Text = contractDetails.EndTime.ToString("HH:mm");

            if (contractDetails.RideType == RideType.InTheCity)
                cbxRideType.SelectedIndex = 0;
            else
                cbxRideType.SelectedIndex = 1;

            lbxCar.SelectedItem = contractDetails.Car;
            lbxRenter.SelectedItem = contractDetails.Renter;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_contractDetails == null)
                _contractDetails = new ContractDetails();

            int.TryParse(tbxDeposit.Text, out var deposit);
            _contractDetails.Deposit = deposit;
            int.TryParse(tbxPrice.Text, out var price);
            _contractDetails.Price = price;
            DateTime.TryParse(tbxConclusionDate.Text, out var conclusiunDate);
            _contractDetails.ConclusionDate = conclusiunDate;
            DateTime.TryParse(tbxEndDate.Text, out var endDate);
            _contractDetails.EndDate = endDate;
            DateTime.TryParse(tbxEndTime.Text, out var endTime);
            _contractDetails.EndTime = endTime;

            _contractDetails.RideType = _rideType[cbxRideType.Text];

            var renter = lbxRenter.SelectedItem as Human;
            var car = lbxCar.SelectedItem as Car;

            _contractDetails.Renter = renter;
            _contractDetails.Car = car;
            _contractDetails.CarId = car.Id;
            _contractDetails.RenterId = renter.Id;

            var contractResult = ModelValidation.Validate(_contractDetails);

            if (contractResult.Count > 0)
                lblError.Content = contractResult.First().ToString();
            else
            {
                AddEditContract();
                lblError.Content = "";
                NavigationService.GoBack();
            }
        }

        private void AddEditContract()
        {
            if (_contractDetails.Id == 0)
                Commands<ContractDetails>.Add(_contractDetails);
            else
                Commands<ContractDetails>.Modify(_contractDetails);

            var replace = new ReplacerWordsInContract();

            try
            {
                replace.Replace(_contractDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void tbxSearchRenter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var renters = new List<Human>();

            if (tbxSearchRenter.Text == "")
                renters = Commands<Human>.SelectAll().Take(3).ToList();
            else
                renters = FindRenter(tbxSearchRenter.Text);

            lbxRenter.ItemsSource = renters;
        }

        private void tbxSearchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cars = new List<Car>();

            if (tbxSearchCar.Text == "")
                cars = Commands<Car>.SelectAll().Take(3).ToList();
            else
                cars = FindCar(tbxSearchCar.Text);

            lbxCar.ItemsSource = cars;
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

        private List<Human> FindRenter(string text)
        {
            var renters = Commands<Human>.SelectAll();
            var rentersResult = new List<Human>();
            var words = text.Split(' ');

            foreach (var renter in renters)
            {
                var renterText = renter.ToString();

                var addToResult = true;
                foreach (var word in words)
                {
                    if (renterText.Contains(word) == false)
                    {
                        addToResult = false;
                        break;
                    }
                }

                if (addToResult == true)
                    rentersResult.Add(renter);
            }

            return rentersResult;
        }
    }
}