using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using CarsRent.LIB.Word;

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

    public ValueTask<List<Car>> UpdateCarsItemsSourceAsync(string searchText, int startPoint, int count)
    {
        var list = new List<Car>();
        if (_contract != null && _contract.CarId.HasValue)
        {
            list.Add(BaseCommands<Car>.SelectById(_contract.CarId));
        }
        
        var cars = BaseCommands<Car>.SelectGroup(startPoint, count, searchText).ToList();

        list.AddRange(cars);
        return new ValueTask<List<Car>>(list);
    }

    public ValueTask<List<Human>> UpdateRentersItemsSourceAsync(string searchText, int startPoint, int count)
    {
        var list = new List<Human>();
        if (_contract != null && _contract.RenterId.HasValue)
        {
            list.Add(BaseCommands<Renter>.SelectById(_contract.RenterId).Human);
        }
        
        var renters = BaseCommands<Renter>.SelectGroup(startPoint, count, searchText).ToList();
        var humans = (from renter in renters select renter.Human).ToList();
        
        list.AddRange(humans);
        return new ValueTask<List<Human>>(list);
    }
    
    public int? GetSelectedRenterId()
    {
        if (_contract == null || _contract.RenterId.HasValue == false)
        {
            return null;
        }

        return _contract.Renter.HumanId;
    }
    
    public int? GetSelectedCarId()
    {
        if (_contract == null || _contract.CarId.HasValue == false)
        {
            return null;
        }

        return _contract.CarId;
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
        if (int.TryParse(valuesDict["price"], out var price) == false)
        {
            return "В поле стоимость поездки введено не число.";
        }
        if (DateTime.TryParse(valuesDict["conclusionDate"], out var conclusiunDate) == false)
        {
            return "В поле дата заключения договора введена не дата.";
        }
        if (DateTime.TryParse(valuesDict["endDate"], out var endDate) == false)
        {
            return "В поле дата окончания договора введена не дата.";
        }
        if (DateTime.TryParse(valuesDict["endTime"], out var endTime) == false)
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

        if (int.TryParse(valuesDict["renter"], out var humanId) == false)
        {
            return "Вы не выбрали арендатора.";
        }
        
        if (int.TryParse(valuesDict["car"], out var carId) == false)
        {
            return "Вы не выбрали автомобиль.";
        }

        var renterId = BaseCommands<Renter>.SelectAll()
            .Where(renter => renter.HumanId == humanId).FirstOrDefault().Id;

        _contract.CarId = carId;
        _contract.RenterId = renterId;

        var contractResult = ModelValidation.Validate(_contract);

        if (contractResult.Count > 0)
        {
            return contractResult.First().ToString();
        }
        
        base.SaveItemInDb(_contract);

        var replacer = new ReplacerWordsInContract();
        replacer.Replace(_contract);

        return string.Empty;
    }
}