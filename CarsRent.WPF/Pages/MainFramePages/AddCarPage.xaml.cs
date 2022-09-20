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
            cbxStatus.ItemsSource = _status.Keys;

            if (car == null)
                return;

            FillFields(car);
            _car = car;
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

            if (car.WheelsType == WheelsType.Summer)
            {
                cbxWheelsType.SelectedIndex = 0;
            }
            else
            {
                cbxWheelsType.SelectedIndex = 1;
            }

            if (car.CarStatus == Status.Ready)
            {
                cbxStatus.SelectedIndex = 0;
            }
            else if(car.CarStatus == Status.OnLease)
            {
                cbxStatus.SelectedIndex = 1;
            }
            else
            {
                cbxStatus.SelectedIndex = 2;
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_car == null)
                _car = new Car();

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

            var carResults = ModelValidation.Validate(_car);

            if (carResults.Count > 0)
                lblError.Content = carResults.First().ToString();
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
                Commands<Car>.Add(_car);
            else
                Commands<Car>.Modify(_car);
        }
    }
}