using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddCarPage : Page
    {
        private Car _car;
        private readonly Dictionary<string, WheelsType> _wheelsType;
        private readonly Dictionary<string, Status> _status;

        public AddCarPage(Car car = null)
        {
            InitializeComponent();

            _wheelsType = new Dictionary<string, WheelsType>
            {
                { "летние", WheelsType.Summer },
                { "зимние", WheelsType.Winter },
            };

            _status = new Dictionary<string, Status>
            {
                { "готова", Status.Ready },
                { "в аренде", Status.OnLease },
                { "в ремонте", Status.UnderRepair },
            };

            CbxWheelsType.ItemsSource = _wheelsType.Keys;
            CbxWheelsType.SelectedIndex = 0;
            CbxStatus.ItemsSource = _status.Keys;
            CbxStatus.SelectedIndex = 0;

            if (car == null)
            {
                car = new Car();
            }

            FillFields(car);
            _car = car;

            UpdateList<Owner>(TbxSearchOwner.Text, LbxOwner, _car.OwnerId);
        }

        private void UpdateList<T>(string text, ListBox lbx, int? id) where T : class, IBaseModel
        {
            if (string.IsNullOrWhiteSpace(text) == false)
            {
                lbx.ItemsSource = Commands<T>.FindAndSelect(text, 0, 3);
            }

            var list = new List<T?>();

            if (id.HasValue)
            {
                list.Add(Commands<T>.SelectById((int)id));
                list.AddRange(Commands<T>.SelectGroup(0, 3).Where(x => x.Id != id).Take(2));
            }
            else
            {
                list.AddRange(Commands<T>.SelectGroup(0, 3));
            }
            
            lbx.ItemsSource = list;
            lbx.SelectedItem = list.Where(x => x?.Id == id);
        }

        private void FillFields(Car car)
        {
            TbxBrand.Text = car.Brand;
            TbxModel.Text = car.Model;
            TbxPassportNumber.Text = car.PassportNumber;
            TbxIssuingDate.Text = car.PassportIssuingDate.ToString("dd.MM.yyyy");
            TbxVin.Text = car.VIN;
            TbxBodyNumber.Text = car.BodyNumber;
            TbxColor.Text = car.Color;
            TbxYear.Text = car.Year.ToString();
            TbxEngineNumber.Text = car.EngineNumber;
            TbxPrice.Text = car.Price.ToString();
            TbxEngineDisplacement.Text = car.EngineDisplacement.ToString();
            TbxRegNumber.Text = car.RegistrationNumber;

            CbxWheelsType.SelectedIndex = car.WheelsType == WheelsType.Summer ? 0 : 1;

            CbxStatus.SelectedIndex = car.CarStatus switch
            {
                Status.Ready => 0,
                Status.OnLease => 1,
                _ => 2
            };
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _car.Brand = TbxBrand.Text;
            _car.Model = TbxModel.Text;
            _car.PassportNumber = TbxPassportNumber.Text;
            _car.VIN = TbxVin.Text;
            _car.BodyNumber = TbxBodyNumber.Text;
            _car.Color = TbxColor.Text;
            _car.EngineNumber = TbxEngineNumber.Text;
            _car.RegistrationNumber = TbxRegNumber.Text;

            int.TryParse(TbxPrice.Text, out var price);
            _car.Price = price;
            int.TryParse(TbxYear.Text, out var year);
            _car.Year = year;
            int.TryParse(TbxEngineDisplacement.Text, out var displacement);
            _car.EngineDisplacement = displacement;
            DateTime.TryParse(TbxIssuingDate.Text, out var issuingDate);
            _car.PassportIssuingDate = issuingDate;

            _car.WheelsType = _wheelsType[CbxWheelsType.Text];
            _car.CarStatus = _status[CbxStatus.Text];
            _car.Owner = LbxOwner.SelectedItem as Owner;

            var carResults = ModelValidation.Validate(_car);

            if (carResults.Count > 0)
            {
                LblError.Content = carResults.First().ToString();
            }
            else
            {
                AddEditCar();
                LblError.Content = "";
                NavigationService.GoBack();
            }
        }

        private void AddEditCar()
        {
            if (_car.Id == 0)
            {
                Commands<Car>.Add(_car);
            }    
            else
            {
                Commands<Car>.Modify(_car);
            }
        }

        private void tbxSearchLandlord_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList<Owner>(TbxSearchOwner.Text, LbxOwner, _car.OwnerId);
        }
    }
}