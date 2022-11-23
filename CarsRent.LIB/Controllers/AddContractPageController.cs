using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Controllers;

public class AddContractPageController : BaseAddEntityController
{
    private Contract? _contract;

    private readonly Dictionary<RideType, string> _rideType;
        
    public AddContractPageController(Contract? contract)
    {
        _rideType = new Dictionary<RideType, string>
        {
            { RideType.InTheCity, "по городу" },
            { RideType.OutsideTheCity, "за городом" }
        };

        _contract = contract;
    }

    public ValueTask<bool> UpdateCarsItemsSourceAsync(string searchText, int startPoint, int count, ref ListBox listBox)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            listBox.ItemsSource = BaseCommands<Car>.FindAndSelectAsync(searchText, startPoint, count).AsTask().Result;
            return new ValueTask<bool>(true);
        }
        
        if (_contract == null || _contract.CarId.HasValue == false)
        {
            listBox.ItemsSource = BaseCommands<Car>.SelectGroupAsync(startPoint, count).AsTask().Result;
            return new ValueTask<bool>(true);
        }

        var selectedCar = BaseCommands<Car>.SelectByIdAsync(_contract.CarId).AsTask().Result;

        var carsList = BaseCommands<Car>.SelectGroupAsync(startPoint, count)
            .AsTask().Result.Where(car => car.Id != selectedCar.Id).Take(2).ToList();
        carsList.Add(selectedCar);

        listBox.ItemsSource = carsList;
        listBox.SelectedItem = selectedCar;

        return new ValueTask<bool>(true);
    }
    
    public ValueTask<bool> UpdateRentersItemsSourceAsync(string searchText, int startPoint, int count, ref ListBox listBox)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            listBox.ItemsSource = HumanCommands.FindAndSelectRentersAsync(searchText, startPoint, count).AsTask().Result;
            return new ValueTask<bool>(true);
        }
        
        if (_contract == null || _contract.RenterId.HasValue == false)
        {
            listBox.ItemsSource = HumanCommands.SelectRentersGroupAsync(startPoint, count).AsTask().Result;
        }

        var selectedRenter = BaseCommands<Renter>.SelectByIdAsync(_contract.RenterId).AsTask().Result;
        var selectedHuman = BaseCommands<Human>.SelectByIdAsync(selectedRenter.HumanId).AsTask().Result;
        
        var humansList = HumanCommands.SelectRentersGroupAsync(startPoint, count).AsTask().Result
            .Where(hum => hum.Id != selectedHuman.Id).Take(2).ToList();
        humansList.Add(selectedHuman);

        listBox.ItemsSource = humansList;
        listBox.SelectedItem = selectedHuman;

        return new ValueTask<bool>(true);
    }

    public void CreateComboBoxesValues(ref ComboBox rideType)
    {
        rideType.ItemsSource = _rideType.Values;
        rideType.SelectedIndex = 0;
    }

    public override string AddEditEntity(UIElementCollection collection, Dictionary<string, string> valuesRelDict)
    {
        var valuesDict = new Dictionary<string, string>(valuesRelDict);
        
        if (int.TryParse(valuesDict["deposit"], out var deposit) == false)
        {
            return "В поле депозит введено не число.";
        }
        if (int.TryParse(valuesDict["price"], out var price))
        {
            return "В поле стоимость поездки введено не число.";
        }
        if (DateTime.TryParse(valuesDict["conclusionDate"], out var conclusiunDate))
        {
            return "В поле дата заключения договора введена не дата.";
        }
        if (DateTime.TryParse(valuesDict["endDate"], out var endDate))
        {
            return "В поле дата окончания договора введена не дата.";
        }
        if (DateTime.TryParse(valuesDict["endTime"], out var endTime))
        {
            return "В поле время окончания договора введено не время.";
        }

        _contract ??= new Contract();

        _contract.RideType = (RideType)int.Parse(valuesDict["rideType"]);
        
        _contract.Deposit = deposit;
        _contract.Price = price;
        _contract.ConclusionDate = conclusiunDate;
        _contract.EndDate = endDate;
        _contract.EndTime = endTime;

        if (int.TryParse(valuesDict["renter"], out var renterId) == false)
        {
            return "Вы не выбрали арендатора.";
        }
        
        if (int.TryParse(valuesDict["car"], out var carId) == false)
        {
            return "Вы не выбрали автомобиль.";
        }
        
        _contract.CarId = carId;
        _contract.RenterId = renterId;

        var contractResult = ModelValidation.Validate(_contract);

        if (contractResult.Count > 0)
        {
            return contractResult.First().ToString();
        }
        
        base.SaveItemInDb(_contract);

        return string.Empty;
    }
}