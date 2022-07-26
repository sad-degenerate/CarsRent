using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using System.Linq;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddCarPage : Page
    {
        private Car _car;

        public AddCarPage(Car car = null)
        {
            InitializeComponent();

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
            tbxIssuingDate.Text = car.PassportIssuingDate;
            tbxVIN.Text = car.VIN;
            tbxBodyNumber.Text = car.BodyNumber;
            tbxColor.Text = car.Color;
            tbxYear.Text = car.Year;
            tbxEngineNumber.Text = car.EngineNumber;
            tbxPrice.Text = car.Price;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_car == null)
                _car = new Car();

            _car.Brand = tbxBrand.Text;
            _car.Model = tbxModel.Text;
            _car.PassportNumber = tbxPassportNumber.Text;
            _car.PassportIssuingDate = tbxIssuingDate.Text;
            _car.VIN = tbxVIN.Text;
            _car.BodyNumber = tbxBodyNumber.Text;
            _car.Color = tbxColor.Text;
            _car.Year = tbxYear.Text;
            _car.EngineNumber = tbxEngineNumber.Text;
            _car.Price = tbxPrice.Text;

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