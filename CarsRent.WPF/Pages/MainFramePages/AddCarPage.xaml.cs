using CarsRent.LIB.Model;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddCarPage : Page
    {
        private AddCarPageController Controller;

        public AddCarPage(Car car = null)
        {
            InitializeComponent();
            
            Controller = new AddCarPageController(car);
            Controller.CreateComboBoxesValues(ref CbxWheelsType, ref CbxStatus);

            UpdateItemsSource();

            if (Controller._car == null)
            {
                return;
            }
            
            FillFields();
        }
        
        private void FillFields()
        {
            var elementCollection = new UIElementCollection(Panel, this);
            var valuesDict = Controller.CreateCarValuesDict();
            
            foreach (var item in elementCollection)
            {
                switch (item)
                {
                    case TextBox textBox:
                        textBox.Text = valuesDict[textBox.Name];
                        break;
                    case ComboBox comboBox when int.TryParse(valuesDict[comboBox.Name], out var index):
                        comboBox.SelectedIndex = index;
                        break;
                }
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // _car.Brand = TbxBrand.Text;
            // _car.Model = TbxModel.Text;
            // _car.PassportNumber = TbxPassportNumber.Text;
            // _car.VIN = TbxVin.Text;
            // _car.BodyNumber = TbxBodyNumber.Text;
            // _car.Color = TbxColor.Text;
            // _car.EngineNumber = TbxEngineNumber.Text;
            // _car.RegistrationNumber = TbxRegNumber.Text;
            //
            // int.TryParse(TbxPrice.Text, out var price);
            // _car.Price = price;
            // int.TryParse(TbxYear.Text, out var year);
            // _car.Year = year;
            // int.TryParse(TbxEngineDisplacement.Text, out var displacement);
            // _car.EngineDisplacement = displacement;
            // DateTime.TryParse(TbxIssuingDate.Text, out var issuingDate);
            // _car.PassportIssuingDate = issuingDate;
            //
            // _car.WheelsType = _wheelsType[CbxWheelsType.Text];
            // _car.CarStatus = _status[CbxStatus.Text];
            // _car.Owner = LbxOwner.SelectedItem as Owner;
            //
            // var carResults = ModelValidation.Validate(_car);
            //
            // if (carResults.Count > 0)
            // {
            //     LblError.Content = carResults.First().ToString();
            // }
            // else
            // {
            //     AddEditCar();
            //     LblError.Content = "";
            //     NavigationService.GoBack();
            // }
        }

        private void AddEditCar()
        {
            // if (_car.Id == 0)
            // {
            //     Commands<Car>.Add(_car);
            // }    
            // else
            // {
            //     Commands<Car>.Modify(_car);
            // }
        }

        private async void UpdateItemsSource()
        {
            var list = await Controller.GetOwners(TbxSearchOwner.Text, 0, 3);
            MessageBox.Show(list.Count.ToString());
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateItemsSource();
        }
    }
}