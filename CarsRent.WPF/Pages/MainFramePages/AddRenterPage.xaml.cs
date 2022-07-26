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
        public AddRenterPage(Human renter = null)
        {
            InitializeComponent();

            if (renter == null)
                return;

            FillField(renter);
            _renter = renter;
        }

        private void FillField(Human renter)
        {
            tbxSurname.Text = renter.Surname;
            tbxName.Text = renter.Name;
            tbxPatronymic.Text = renter.Patronymic;
            tbxBirthDate.Text = renter.BirthDate.ToString();
            tbxPassportNumber.Text = renter.IdentityNumber;
            tbxIssuingOrganization.Text = renter.IssuingOrganization;
            tbxIssuingDate.Text = renter.IssuingDate.ToString();
            tbxRegistrationPlace.Text = renter.RegistrationPlace;
            tbxPhone.Text = renter.PhoneNumber;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_renter == null)
                _renter = new Human();

            _renter.Name = tbxName.Text;
            _renter.Surname = tbxSurname.Text;
            _renter.Patronymic = tbxPatronymic.Text;
            _renter.BirthDate = tbxBirthDate.Text;
            _renter.PhoneNumber = tbxPhone.Text;
            _renter.IdentityNumber = tbxPassportNumber.Text;
            _renter.IssuingOrganization = tbxIssuingOrganization.Text;
            _renter.IssuingDate = tbxIssuingDate.Text;
            _renter.RegistrationPlace = tbxRegistrationPlace.Text;

            var renterResults = ModelValidation.Validate(_renter);

            if (renterResults.Count > 0)
                lblError.Content = renterResults.First().ToString();
            else
            {
                AddEditRenter();
                lblError.Content = "";
                NavigationService.GoBack();
            }
        }

        private void AddEditRenter()
        {
            if (_renter.Id == 0)
                Commands<Human>.Add(_renter);
            else
                Commands<Human>.Modify(_renter);
        }
    }
}