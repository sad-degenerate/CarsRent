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

            if (contractDetails != null)
            {
                FillField(contractDetails);
                _contractDetails = contractDetails;
            }
            else
            {
                _contractDetails = new ContractDetails();
            }

            _rideType = new Dictionary<string, RideType>
            {
                { "по городу", RideType.InTheCity },
                { "за городом", RideType.OutsideTheCity }
            };

            cbxRideType.ItemsSource = _rideType.Keys;
            cbxRideType.SelectedIndex = 0;

            UpdateList<Car>(tbxSearchCar.Text, lbxCar, _contractDetails.CarId);
            UpdateList<Human>(tbxSearchRenter.Text, lbxRenter, _contractDetails.RenterId);
        }

        private void UpdateList<T>(string text, ListBox lbx, int? id) where T: class, IBaseModel
        {
            if (text != string.Empty)
            {
                lbx.ItemsSource = Commands<T>.FindAndSelect(text, 0, 3).ToList();
            }
            else if (id.HasValue == true)
            {
                var list = new List<T>();
                var activeItem = Commands<T>.SelectById((int)id);
                var items = Commands<T>.SelectGroup(1, 3);
                
                list.Add(activeItem);
                foreach (var item in items)
                {
                    if (item.Id != activeItem.Id && list.Count < 3)
                    {
                        list.Add(item);
                    }
                }

                lbx.ItemsSource = list;
                lbx.SelectedItem = activeItem;
            }
            else
            {
                lbx.ItemsSource = Commands<T>.SelectGroup(1, 3).ToList();
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
            UpdateList<Human>(tbxSearchRenter.Text, lbxRenter, _contractDetails.RenterId);
        }

        private void tbxSearchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList<Car>(tbxSearchCar.Text, lbxCar, _contractDetails.CarId);
        }
    }
}