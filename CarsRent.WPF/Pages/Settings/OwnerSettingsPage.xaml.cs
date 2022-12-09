using CarsRent.LIB.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.WPF.ViewControllers;

namespace CarsRent.WPF.Pages.Settings
{
    public partial class OwnerSettingsPage : Page
    {
        // TODO: брать из настроек
        private int _pageSize = 10;
        private int _currentPage = 1;
        private int? _ownerId;
        
        public OwnerSettingsPage()
        {
            InitializeComponent();
            UpdateOwners();
        }

        private async void UpdateOwners()
        {
            var startPoint = 0;
            if (_currentPage != 1)
            {
                startPoint = _currentPage * _pageSize;
            }

            var list = await OwnersSettings.GetOwners(TbxSearchOwner.Text, startPoint, _pageSize);
            
            LbxOwner.ItemsSource = list;
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateOwners();
        }

        private void ButtonAddEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var controller = new FillingRenterFieldsController();
            var fields = new Dictionary<string, string>(controller.CreateValuesRelationDict(Panel.Children));

            var error = OwnersSettings.AddOwner(fields, _ownerId);

            if (string.IsNullOrWhiteSpace(error))
            {
                UpdateOwners();
                _ownerId = null;
                
                LblAddError.Content = string.Empty;
                LblAddDone.Content = "Арендодатель успешно добавлен/изменен.";
                LblChooseStatus.Content = "Текущий режим: добавление";

                return;
            }

            LblAddDone.Content = string.Empty;
            LblAddError.Content = error;
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var error = OwnersSettings.DeleteOwner(LbxOwner.SelectedItem);
            
            if (string.IsNullOrWhiteSpace(error))
            {
                UpdateOwners();
                LblChooseError.Content = string.Empty;
                LblChooseDone.Content = "Арендодатель успешно удален.";
                return;
            }

            LblChooseDone.Content = string.Empty;
            LblChooseError.Content = error;
        }

        private void ButtonModify_OnClick(object sender, RoutedEventArgs e)
        {
            if (LbxOwner.SelectedItem is not Human human)
            {
                return;
            }

            var controller = new FillingRenterFieldsController();
            var valuesRelDict = controller.CreateValuesRelationDict(human);
            var collection = Panel.Children;
            
            controller.FillFields(ref collection, valuesRelDict);

            var owner = BaseCommands<Owner>.SelectAllAsync().AsTask().Result
                .Where(owner => owner.HumanId == human.Id).FirstOrDefault();

            _ownerId = owner.Id;
            LblChooseStatus.Content = "Текущий режим: редактирование";
        }

        private void ButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            PaginationGo(_currentPage + 1);
        }

        private void ButtonLeft_OnClick(object sender, RoutedEventArgs e)
        {
            PaginationGo(_currentPage - 1);
        }

        private void PaginationGo(int newCurrentPage)
        {
            if (newCurrentPage <= 0)
            {
                return;
            }

            _currentPage = newCurrentPage;
            UpdateOwners();

            LblPage.Content = $"Текщая страница: {_currentPage}";
        }
    }
}