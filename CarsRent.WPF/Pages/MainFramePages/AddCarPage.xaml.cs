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

            cbxWheelsType.ItemsSource = _wheelsType.Keys;
            cbxWheelsType.SelectedIndex = 0;
            cbxStatus.ItemsSource = _status.Keys;
            cbxStatus.SelectedIndex = 0;

            if (car == null)
            {
                car = new Car();
            }

            FillFields(car);
            _car = car;

            UpdateList<Owner>(tbxSearchOwner.Text, lbxOwner, _car.OwnerId);
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
            tbxBrand.Text = car.Brand;
            tbxModel.Text = car.Model;
            tbxPassportNumber.Text = car.PassportNumber;
            tbxIssuingDate.Text = car.PassportIssuingDate.ToString("dd.MM.yyyy");
            tbxVIN.Text = car.VIN;
            tbxBodyNumber.Text = car.BodyNumber;
            tbxColor.Text = car.Color;
            tbxYear.Text = car.Year.ToString();
            tbxEngineNumber.Text = car.EngineNumber;
            tbxPrice.Text = car.Price.ToString();
            tbxEngineDisplacement.Text = car.EngineDisplacement.ToString();
            tbxRegNumber.Text = car.RegistrationNumber;

            cbxWheelsType.SelectedIndex = car.WheelsType == WheelsType.Summer ? 0 : 1;

            cbxStatus.SelectedIndex = car.CarStatus switch
            {
                Status.Ready => 0,
                Status.OnLease => 1,
                _ => 2
            };
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _car.Brand = tbxBrand.Text;
            _car.Model = tbxModel.Text;
            _car.PassportNumber = tbxPassportNumber.Text;
            _car.VIN = tbxVIN.Text;
            _car.BodyNumber = tbxBodyNumber.Text;
            _car.Color = tbxColor.Text;
            _car.EngineNumber = tbxEngineNumber.Text;
            _car.RegistrationNumber = tbxRegNumber.Text;

            int.TryParse(tbxPrice.Text, out var price);
            _car.Price = price;
            int.TryParse(tbxYear.Text, out var year);
            _car.Year = year;
            int.TryParse(tbxEngineDisplacement.Text, out var displacement);
            _car.EngineDisplacement = displacement;
            DateTime.TryParse(tbxIssuingDate.Text, out var issuingDate);
            _car.PassportIssuingDate = issuingDate;

            _car.WheelsType = _wheelsType[cbxWheelsType.Text];
            _car.CarStatus = _status[cbxStatus.Text];
            _car.Owner = lbxOwner.SelectedItem as Owner;

            var carResults = ModelValidation.Validate(_car);

            if (carResults.Count > 0)
            {
                lblError.Content = carResults.First().ToString();
            }
            else
            {
                AddEditCar();
                lblError.Content = "";
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
            UpdateList<Owner>(tbxSearchOwner.Text, lbxOwner, _car.OwnerId);
        }
    }
}