using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class Renters : Page
    {
        private List<Human> _renters;
        public Renters()
        {
            InitializeComponent();

            _renters = Commands<Human>.Select(1, 1).ToList();
            
            AddRentersToView(_renters);
        }

        private void AddRentersToView(List<Human> renters)
        {
            var context = ApplicationContext.Instance();
            var data = context.Humans.Join(
                context.Passports,
                human => human.Id,
                passport => passport.Human.Id,
                (human, passport) => new
                {
                    human.Surname,
                    human.Name,
                    human.Patronymic,
                    human.BirthDate,

                    passport.IdentityNumber,
                    passport.IssuingDate,
                    passport.IssuingOrganization,
                    passport.RegistrationPlace,
                }).ToList();

            dgRenters.ItemsSource = data;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRenterPage());
        }
    }
}