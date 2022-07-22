using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddRenterPage : Page
    {
        public AddRenterPage(Human renter = null)
        {
            InitializeComponent();

            if (renter == null)
                return;

            FillField(renter);
        }

        private void FillField(Human renter)
        {
            tbxSurname.Text = renter.Surname;
            tbxName.Text = renter.Name;
            tbxPatronymic.Text = renter.Patronymic;
            tbxBirthDate.Text = renter.BirthDate.ToString();
            tbxPassportNumber.Text = renter.Passport.IdentityNumber;
            tbxIssuingOrganization.Text = renter.Passport.IssuingOrganization;
            tbxIssuingDate.Text = renter.Passport.IssuingDate.ToString();
            tbxRegistrationPlace.Text = renter.Passport.RegistrationPlace;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var renter = new Human();
            renter.Name = tbxName.Text;
            renter.Surname = tbxSurname.Text;
            renter.Patronymic = tbxPatronymic.Text;
            renter.BirthDate = tbxBirthDate.Text;
            renter.PhoneNumber = tbxPhone.Text;

            var passport = new Passport();
            passport.IdentityNumber = tbxPassportNumber.Text;
            passport.IssuingOrganization = tbxIssuingOrganization.Text;
            passport.IssuingDate = tbxIssuingDate.Text;
            passport.RegistrationPlace = tbxRegistrationPlace.Text;
            
            renter.Passport = passport;

            var passportResults = Validate(passport);
            var renterResults = Validate(renter);

            // TODO: заменить MessageBox на что-то более удобное

            if (passportResults.Count > 0)
                MessageBox.Show(passportResults.First().ToString());
            else if (renterResults.Count > 0)
                MessageBox.Show(renterResults.First().ToString());
            else
            {
                AddRenter(renter);
                MessageBox.Show("Арендатор успешно добавлен");
            }
        }

        private static List<System.ComponentModel.DataAnnotations.ValidationResult> Validate<T>(T obj)
        {
            var validationContext = new ValidationContext(obj); ;
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, results, true);
            return results;
        }

        private void AddRenter(Human renter)
        {
            Commands<Human>.Add(renter);
        }
    }
}