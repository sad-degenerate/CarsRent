using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddRenterPage : Page
    {
        private AddRenterPageController _controller;
        public AddRenterPage(Human? renter = null)
        {
            InitializeComponent();

            _controller = new AddRenterPageController(renter);

            if (_controller.Renter != null)
            {
                FillFields();
            }
        }

        private void FillFields()
        {
            var elementCollection = new UIElementCollection(Panel, this);
            var valuesDict = _controller.CreateRenterValuesRelationDict();
            
            foreach (var item in elementCollection)
            {
                if (item is TextBox textBox)
                {
                    textBox.Text = valuesDict[textBox.Name];
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var errors = new List<string>();

            if (DateTime.TryParse(TbxIssuingDate.Text, out var issuingDate) == false)
            {
                errors.Add("В поле дата выдачи паспорта введена не дата.");
            }
            
            if (DateTime.TryParse(TbxBirthDate.Text, out var birthDate) == false)
            {
                errors.Add("В поле дата рождения введена не дата.");
            }

            if (errors.Any())
            {
                LblError.Content = errors.FirstOrDefault();
                return;
            }

            var renter = new Human
            {
                BirthDate = birthDate,
                PassportNumber = TbxPassportNumber.Text,
            };
            
            if (LbxOwner.SelectedItem is not Human)
            {
                errors.Add("Вы не выбрали владельца автомобиля.");
            }

            var owner = LbxOwner.SelectedItem as Human;
            
            errors.Add(_controller.AddEditCarAsync(renter, owner).AsTask().Result);

            if (errors.Any())
            {
                LblError.Content = errors.FirstOrDefault();
                return;
            }
            
            NavigationService.GoBack();
        }

        private void AddEditRenter()
        {
            if (_controller.Id == 0)
            {
                var renter = new Renter
                {
                    Human = _controller
                };
                BaseCommands<Human>.AddAsync(_controller);
                BaseCommands<Renter>.AddAsync(renter);
            }
            else
            {
                BaseCommands<Human>.ModifyAsync(_controller);
            }
        }
    }
}