using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Controllers;

public class AddCarPageController : BaseAddEntityController
{
    private Car? _car;

    private readonly Dictionary<WheelsType, string> _wheelsType;
    private readonly Dictionary<Status, string> _status;

    public AddCarPageController(Car? car)
    {
        _wheelsType = new Dictionary<WheelsType, string>
        {
            { WheelsType.Summer, "летние"  },
            { WheelsType.Winter, "зимние"  }
        };

        _status = new Dictionary<Status, string>
        {
            { Status.Ready, "готова" },
            { Status.OnLease, "в аренде" },
            { Status.UnderRepair, "в ремонте" }
        };

        _car = car;
    }

    public void CreateComboBoxesValues(ref ComboBox wheelsType, ref ComboBox carStatus)
    {
        wheelsType.ItemsSource = _wheelsType.Values;
        wheelsType.SelectedIndex = 0;
        carStatus.ItemsSource = _status.Values;
        carStatus.SelectedIndex = 0;
    }

    public ValueTask<List<Human>> UpdateOwnersItemsSourceAsync(string searchText, int startPoint, int count)
    {
        var list = new List<Human>();
        if (_car != null && _car.OwnerId.HasValue)
        {
            list.Add(BaseCommands<Owner>.SelectById(_car.OwnerId).Human);
        }
        
        var owners = BaseCommands<Owner>.SelectGroup(startPoint, count, searchText).ToList();
        var humans = (from owner in owners select owner.Human).ToList();
        
        list.AddRange(humans);
        return new ValueTask<List<Human>>(list);
    }

    public int? GetSelectedOwnerId()
    {
        if (_car == null || _car.OwnerId.HasValue == false)
        {
            return null;
        }

        return _car.Owner.HumanId;
    }

    public override string AddEditEntity(UIElementCollection collection, Dictionary<string, string> valuesRelDict)
    {
        var valuesDict = new Dictionary<string, string>(valuesRelDict);

        if (int.TryParse(valuesDict["price"], out var price) == false)
        {
            return "В поле стоимость автомобиля введено не число.";
        }

        if (int.TryParse(valuesDict["year"], out var year) == false)
        {
            return "В поле год выпуска автомобиля введено не число.";
        }

        if (int.TryParse(valuesDict["engineDisplacement"], out var displacement) == false)
        {
            return "В поле рабочий объем двигателя автомобиля введено не число.";
        }

        if (DateTime.TryParse(valuesDict["passportIssuingDate"], out var issuingDate) == false)
        {
            return "В поле дата выдачи паспорта автомобиля введена не дата.";
        }
        
        if (int.TryParse(valuesDict["owner"], out var humanId) == false)
        {
            return "Вы не выбрали арендодателя.";
        }
        
        var owner = BaseCommands<Owner>.SelectAll()
            .Where(owner => owner.HumanId == humanId).FirstOrDefault();

        _car ??= new Car();

        _car.Brand = valuesDict["brand"];
        _car.Model = valuesDict["model"];
        _car.PassportNumber = valuesDict["passportNumber"];
        _car.VIN = valuesDict["vin"];
        _car.BodyNumber = valuesDict["bodyNumber"];
        _car.Color = valuesDict["color"];
        _car.EngineNumber = valuesDict["engineNumber"];
        _car.RegistrationNumber = valuesDict["registrationNumber"];
        _car.Price = price;
        _car.Year = year;
        _car.EngineDisplacement = displacement;
        _car.PassportIssuingDate = issuingDate;
        _car.WheelsType = (WheelsType)int.Parse(valuesDict["wheelsType"]);
        _car.CarStatus = (Status)int.Parse(valuesDict["status"]);
        _car.OwnerId = owner.Id;

        var validationResults = ModelValidation.Validate(_car);
            
        if (validationResults.Any())
        {
            return validationResults.First().ErrorMessage;
        }

        base.SaveItemInDb(_car);
        
        return string.Empty;
    }
}