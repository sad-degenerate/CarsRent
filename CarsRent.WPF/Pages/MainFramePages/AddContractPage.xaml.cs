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
        private readonly Contract _contract;
        private readonly Dictionary<string, RideType> _rideType;

        public AddContractPage(Contract contract = null)
        {
            InitializeComponent();

            _rideType = new Dictionary<string, RideType>
            {
                { "по городу", RideType.InTheCity },
                { "за городом", RideType.OutsideTheCity }
            };

            cbxRideType.ItemsSource = _rideType.Keys;
            cbxRideType.SelectedIndex = 0;

            UpdateList<Car>(tbxSearchCar.Text, lbxCar, _contract.CarId);
            UpdateList<Human>(tbxSearchRenter.Text, lbxRenter, _contract.RenterId);

            if (contract == null)
            {
                return;
            }
            
            FillField(contract);
            _contract = contract;
        }

        private void UpdateList<T>(string text, ListBox lbx, int? id) where T: class, IBaseModel
        {
            if (text != string.Empty)
            {
                lbx.ItemsSource = Commands<T>.FindAndSelect(text, 0, 3);
            }

            var list = new List<T?>();

            if (id.HasValue)
            {
                list.Add(Commands<T>.SelectById((int)id));
                list.AddRange(Commands<T>.SelectGroup(0, 3).Where(x => x.Id != id).Take(2));
            }

            list.AddRange(Commands<T>.SelectGroup(0, 3));

            lbx.ItemsSource = list;
            lbx.SelectedItem = list.Where(x => x.Id != id);
        }

        private void FillField(Contract contract)
        {
            tbxDeposit.Text = contract.Deposit.ToString();
            tbxPrice.Text = contract.Price.ToString();
            tbxConclusionDate.Text = contract.ConclusionDate.ToString("dd.MM.yyyy");
            tbxEndDate.Text = contract.EndDate.ToString("dd.MM.yyyy");
            tbxEndTime.Text = contract.EndTime.ToString("HH:mm");

            cbxRideType.SelectedIndex = contract.RideType == RideType.InTheCity ? 0 : 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO: убрать
            if (int.TryParse(tbxDeposit.Text, out var deposit))
            {
                _contract.Deposit = deposit;
            }
            if (int.TryParse(tbxPrice.Text, out var price))
            {
                _contract.Price = price;
            }
            if (DateTime.TryParse(tbxConclusionDate.Text, out var conclusiunDate))
            {
                _contract.ConclusionDate = conclusiunDate;
            }
            if (DateTime.TryParse(tbxEndDate.Text, out var endDate))
            {
                _contract.EndDate = endDate;
            }
            if (DateTime.TryParse(tbxEndTime.Text, out var endTime))
            {
                _contract.EndTime = endTime;
            }

            _contract.RideType = _rideType[cbxRideType.Text];

            var renter = lbxRenter.SelectedItem as Renter;
            var car = lbxCar.SelectedItem as Car;

            _contract.Renter = renter;
            _contract.Car = car;
            _contract.CarId = car.Id;
            _contract.RenterId = renter.Id;

            var contractResult = ModelValidation.Validate(_contract);

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
            if (_contract.Id == 0)
            {
                Commands<Contract>.Add(_contract);
            }
            else
            {
                Commands<Contract>.Modify(_contract);
            }

            var replace = new ReplacerWordsInContract();

            try
            {
                replace.Replace(_contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void tbxSearchRenter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList<Renter>(tbxSearchRenter.Text, lbxRenter, _contract.RenterId);
        }

        private void tbxSearchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList<Car>(tbxSearchCar.Text, lbxCar, _contract.CarId);
        }
    }
}