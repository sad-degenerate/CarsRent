﻿using System.Collections.Generic;
using System.Windows;
using CarsRent.LIB.Model;
using System.Windows.Controls;
using CarsRent.LIB.Controllers;
using CarsRent.WPF.ViewControllers;

namespace CarsRent.WPF.Pages.MainFramePages
{
    public partial class AddCarPage : Page
    {
        private readonly AddCarPageController _addEditController;
        private readonly FillingCarFieldsController _fillingFieldsController;

        public AddCarPage(Car? car = null)
        {
            InitializeComponent();
            
            _addEditController = new AddCarPageController(car);
            _fillingFieldsController = new FillingCarFieldsController();
            
            _addEditController.CreateComboBoxesValues(ref CbxWheelsType, ref CbxStatus);
            UpdateItemsSource();

            if (car == null)
            {
                return;
            }

            var collection = Panel.Children;
            var valuesRelDict = _fillingFieldsController.CreateValuesRelationDict(car);
            _fillingFieldsController.FillFields(ref collection, valuesRelDict);
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddEditCar();
        }

        private void AddEditCar()
        {
            BtnSave.IsEnabled = false;

            var collection = Panel.Children;
            var valuesRelDict = new Dictionary<string, string>(_fillingFieldsController.CreateValuesRelationDict(collection));
            var error = _addEditController.AddEditEntity(collection, valuesRelDict);

            if (string.IsNullOrWhiteSpace(error) == false)
            {
                MessageBox.Show(error, "Не удалось добавить автомобиль.");
            
                BtnSave.IsEnabled = true;
            }
            else
            {
                NavigationService.GoBack();
            }
        }

        private async void UpdateItemsSource()
        {
            LbxOwner.ItemsSource = await _addEditController.UpdateOwnersItemsSourceAsync(TbxSearchOwner.Text, 0, 3);

            var selectedItemId = _addEditController.GetSelectedOwnerId();
            if (selectedItemId.HasValue == false)
            {
                return;
            }

            foreach (var item in LbxOwner.ItemsSource)
            {
                if (item is not Human human || human.Id != selectedItemId)
                {
                    continue;
                }

                LbxOwner.SelectedItem = item;
            }
        }

        private void tbxSearchOwner_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateItemsSource();
        }
    }
}