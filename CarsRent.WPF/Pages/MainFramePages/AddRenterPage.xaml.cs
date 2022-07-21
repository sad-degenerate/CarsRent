using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            
            var surname = tbxSurname.Text;
            var name = tbxName.Text;
            var patronymic = tbxPatronymic.Text;
            var birthDate = tbxBirthDate.Text;
            var passportNumber = tbxPassportNumber.Text;
            var issuingOrganization = tbxIssuingOrganization.Text;
            var issuingDate = tbxIssuingDate.Text;
            var registrationPlace = tbxRegistrationPlace.Text;

            // TODO: DevExpress

            var renter = new Human();
            renter.Name = name;
            renter.Surname = surname;
            renter.Patronymic = patronymic;
            renter.BirthDate = birthDate;

            var passport = new Passport();
            passport.IdentityNumber = passportNumber;
            passport.IssuingOrganization = issuingOrganization;
            DateTime.TryParse(issuingDate, out var date);
            passport.IssuingDate = date;
            passport.RegistrationPlace = registrationPlace;

            renter.PhoneNumber = "89039328345";
            renter.Passport = passport;

            var passportValidation = new ValidationContext(passport);
            var renterValidation = new ValidationContext(renter);

            var passportResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var renterResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            if (Validator.TryValidateObject(passport, passportValidation, passportResults, true))
                MessageBox.Show("OK");
            else
            {
                var errors = "";
                foreach (var res in passportResults)
                    errors += $"{res.ErrorMessage}\n";
                MessageBox.Show(errors);
            }

            if (Validator.TryValidateObject(renter, renterValidation, renterResults, true))
                MessageBox.Show("OK");
            else
            {
                var errors = "";
                foreach (var res in renterResults)
                    errors += $"{res.ErrorMessage}\n";
                MessageBox.Show(errors);
            }
        }
    }
}