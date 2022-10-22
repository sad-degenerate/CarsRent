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

            CbxRideType.ItemsSource = _rideType.Keys;
            CbxRideType.SelectedIndex = 0;

            if (contract != null)
            {
                FillField(contract);
                _contract = contract;
            }

            _contract = new Contract();
            
            UpdateList<Car>(TbxSearchCar.Text, LbxCar, _contract.CarId);
            UpdateList<Renter>(TbxSearchRenter.Text, LbxRenter, _contract.RenterId);
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
            TbxDeposit.Text = contract.Deposit.ToString();
            TbxPrice.Text = contract.Price.ToString();
            TbxConclusionDate.Text = contract.ConclusionDate.ToString("dd.MM.yyyy");
            TbxEndDate.Text = contract.EndDate.ToString("dd.MM.yyyy");
            TbxEndTime.Text = contract.EndTime.ToString("HH:mm");

            CbxRideType.SelectedIndex = contract.RideType == RideType.InTheCity ? 0 : 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO: убрать
            if (int.TryParse(TbxDeposit.Text, out var deposit))
            {
                _contract.Deposit = deposit;
            }
            if (int.TryParse(TbxPrice.Text, out var price))
            {
                _contract.Price = price;
            }
            if (DateTime.TryParse(TbxConclusionDate.Text, out var conclusiunDate))
            {
                _contract.ConclusionDate = conclusiunDate;
            }
            if (DateTime.TryParse(TbxEndDate.Text, out var endDate))
            {
                _contract.EndDate = endDate;
            }
            if (DateTime.TryParse(TbxEndTime.Text, out var endTime))
            {
                _contract.EndTime = endTime;
            }

            _contract.RideType = _rideType[CbxRideType.Text];

            var renter = LbxRenter.SelectedItem as Renter;
            var car = LbxCar.SelectedItem as Car;

            _contract.Renter = renter;
            _contract.Car = car;
            _contract.CarId = car.Id;
            _contract.RenterId = renter.Id;

            var contractResult = ModelValidation.Validate(_contract);

            if (contractResult.Count > 0)
            {
                LblError.Content = contractResult.First().ToString();
            }
            else
            {
                AddEditContract();
                LblError.Content = "";
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
            UpdateList<Renter>(TbxSearchRenter.Text, LbxRenter, _contract.RenterId);
        }

        private void tbxSearchCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList<Car>(TbxSearchCar.Text, LbxCar, _contract.CarId);
        }
    }
}