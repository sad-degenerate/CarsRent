using CarsRent.LIB.Attributes;
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

            if (contractDetails == null)
            {
                return;
            }

            _rideType = new Dictionary<string, RideType>
            {
                { "по городу", RideType.InTheCity },
                { "за городом", RideType.OutsideTheCity }
            };

            cbxRideType.ItemsSource = _rideType.Keys;

            UpdateCarList();
            UpdateRenterList();

            FillField(contractDetails);
            _contractDetails = contractDetails;
        }

        private void UpdateRenterList()
        {
            if (tbxSearchRenter.Text == string.Empty)
            {
                lbxRenter.ItemsSource = Commands<Human>.SelectGroup(1, 3);
            }
            else
            {
                lbxRenter.ItemsSource = Commands<Human>.FindAndSelect(tbxSearchRenter.Text, 1, 3);
            }
        }

        private void UpdateCarList()
        {
            if (tbxSearchCar.Text == string.Empty)
            {
                lbxCar.ItemsSource = Commands<Car>.SelectGroup(1, 3);
            }
            else
            {
                lbxCar.ItemsSource = Commands<Car>.FindAndSelect(tbxSearchCar.Text, 1, 3);
            }
        }

        private void FillField(ContractDetails contractDetails)
        {
            tbxDeposit.Text = contractDetails.Deposit.ToString();
            tbxPrice.Text = contractDetails.Price.ToString();
            tbxConclusionDate.Text = contractDetails.ConclusionDate.ToString("dd.MM.yyyy");
            tbxEndDate.Text = contractDetails.EndDate.ToString("dd.MM.yyyy");
            tbxEndTime.Text = contractDetails.EndTime.ToString("HH:mm");

            if (contractDetails.RideType == RideType.InTheCity)
            {
                cbxRideType.SelectedIndex = 0;
            }
            else
            {
                cbxRideType.SelectedIndex = 1;
            }

            lbxCar.SelectedItem = contractDetails.Car;
            lbxRenter.SelectedItem = contractDetails.Renter;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _contractDetails ??= new ContractDetails();

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
            {
                lblError.Content = contractResult.First().ToString();
            }
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
            {
                Commands<ContractDetails>.Add(_contractDetails);
            }
            else
            {
                Commands<ContractDetails>.Modify(_contractDetails);
            }

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
            UpdateRenterList();
        }

        private void tbxSearchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCarList();
        }
    }
}