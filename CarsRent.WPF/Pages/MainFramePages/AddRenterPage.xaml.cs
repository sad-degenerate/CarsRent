using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddRenterPage : Page
    {
        private Human _renter;
        public AddRenterPage(Human? renter = null)
        {
            InitializeComponent();

            if (renter == null)
            {
                return;
            }
            
            FillField(renter);
            _renter = renter;
        }

        private void FillField(Human renter)
        {
            TbxSurname.Text = renter.Surname;
            TbxName.Text = renter.Name;
            TbxPatronymic.Text = renter.Patronymic;
            TbxBirthDate.Text = renter.BirthDate.ToString("dd.MM.yyyy");
            TbxPassportNumber.Text = renter.PassportNumber;
            TbxIssuingOrganization.Text = renter.IssuingOrganization;
            TbxIssuingDate.Text = renter.IssuingDate.ToString("dd.MM.yyyy");
            TbxRegistrationPlace.Text = renter.RegistrationPlace;
            TbxPhone.Text = renter.PhoneNumber;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _renter ??= new Human();

            _renter.Name = TbxName.Text;
            _renter.Surname = TbxSurname.Text;
            _renter.Patronymic = TbxPatronymic.Text;
            _renter.PhoneNumber = TbxPhone.Text;
            _renter.PassportNumber = TbxPassportNumber.Text;
            _renter.IssuingOrganization = TbxIssuingOrganization.Text;
            _renter.RegistrationPlace = TbxRegistrationPlace.Text;

            DateTime.TryParse(TbxIssuingDate.Text, out var issuingDate);
            _renter.IssuingDate = issuingDate;
            DateTime.TryParse(TbxBirthDate.Text, out var birthDate);
            _renter.BirthDate = birthDate;

            var renterResults = ModelValidation.Validate(_renter);

            if (renterResults.Count > 0)
            {
                LblError.Content = renterResults.First().ToString();
            }
            else
            {
                AddEditRenter();
                LblError.Content = "";
                NavigationService.GoBack();
            }
        }

        private void AddEditRenter()
        {
            if (_renter.Id == 0)
            {
                var renter = new Renter
                {
                    Human = _renter
                };
                Commands<Human>.Add(_renter);
                Commands<Renter>.Add(renter);
            }
            else
            {
                Commands<Human>.Modify(_renter);
            }
        }
    }
}