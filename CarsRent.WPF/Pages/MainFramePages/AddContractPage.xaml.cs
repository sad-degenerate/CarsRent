using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddContractPage : Page
    {
        private ContractDetails _contractDetails;
        private readonly Dictionary<string, RideType> _rideType = new Dictionary<string, RideType>();

        public AddContractPage(ContractDetails contractDetails = null)
        {
            InitializeComponent();

            _rideType.Add("по городу", RideType.InTheCity);
            _rideType.Add("за городом", RideType.OutsideTheCity);

            cbxRideType.ItemsSource = _rideType.Keys;
            cbxCar.ItemsSource = Commands<Car>.SelectAll().ToList();
            cbxRenter.ItemsSource = Commands<Human>.SelectAll().ToList();

            if (contractDetails == null)
                return;

            FillField(contractDetails);
            _contractDetails = contractDetails;
        }

        private void FillField(ContractDetails contractDetails)
        {
            tbxDeposit.Text = contractDetails.Deposit;
            tbxPrice.Text = contractDetails.Price;
            tbxConclusionDate.Text = contractDetails.ConclusionDate;
            tbxEndDate.Text = contractDetails.EndDate;

            if (contractDetails.RideType == RideType.InTheCity)
                cbxRideType.SelectedIndex = 0;
            else
                cbxRideType.SelectedIndex = 1;

            cbxCar.SelectedItem = contractDetails.Car;
            cbxRenter.SelectedItem = contractDetails.Renter;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_contractDetails == null)
                _contractDetails = new ContractDetails();

            _contractDetails.Deposit = tbxDeposit.Text;
            _contractDetails.Price = tbxPrice.Text;
            _contractDetails.ConclusionDate = tbxConclusionDate.Text;
            _contractDetails.EndDate = tbxEndDate.Text;

            _contractDetails.RideType = _rideType[cbxRideType.Text];

            var renter = cbxRenter.SelectedItem as Human;
            var car = cbxCar.SelectedItem as Car;

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
        }
    }
}