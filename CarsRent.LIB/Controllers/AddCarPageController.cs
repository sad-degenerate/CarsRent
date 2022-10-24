using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Controllers;

public class AddCarPageController : BaseAddEntityController
{
    public Car? Car { get; }

    private readonly Dictionary<string, WheelsType> _wheelsType;
    private readonly Dictionary<string, Status> _status;

    public AddCarPageController(Car? car)
    {
        _wheelsType = new Dictionary<string, WheelsType>
        {
            { "летние", WheelsType.Summer },
            { "зимние", WheelsType.Winter },
        };

        _status = new Dictionary<string, Status>
        {
            { "готова", Status.Ready },
            { "в аренде", Status.OnLease },
            { "в ремонте", Status.UnderRepair },
        };

        Car = car;
    }

    public void CreateComboBoxesValues(ref ComboBox wheelsType, ref ComboBox carStatus)
    {
        wheelsType.ItemsSource = _wheelsType.Keys;
        wheelsType.SelectedIndex = 0;
        carStatus.ItemsSource = _status.Keys;
        carStatus.SelectedIndex = 0;
    }

    public void FillFields(ref UIElementCollection collection)
    {
        var valuesDict = CreateValuesRelationDict(Car);
        
        base.FillFields(ref collection, valuesDict);
    }

    public ValueTask<List<Human>> GetOwnersAsync(string searchText, int startPoint, int count)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return HumanCommands.FindAndSelectOwnersAsync(searchText, startPoint, count);
        }
        
        if (Car == null || Car.OwnerId.HasValue == false)
        {
            return HumanCommands.SelectOwnersGroupAsync(startPoint, count);
        }

        var owner = BaseCommands<Owner>.SelectByIdAsync(Car.OwnerId).AsTask().Result;
        var human = BaseCommands<Human>.SelectByIdAsync(owner.HumanId).AsTask().Result;
        var list = new List<Human>
        {
            human
        };
        var otherHumans = HumanCommands.SelectOwnersGroupAsync(startPoint, count).AsTask().Result;
        otherHumans = otherHumans.Where(hum => hum.Id != human.Id).ToList();
        list.AddRange(otherHumans);

        return new ValueTask<List<Human>>(list);
    }

    public void SelectListBoxSelectedOwner(ref ListBox listBox)
    {
        if (Car == null || Car.OwnerId.HasValue == false)
        {
            return;
        }

        var owner = BaseCommands<Owner>.SelectByIdAsync(Car.OwnerId).AsTask().Result;
        var human = BaseCommands<Human>.SelectByIdAsync(owner.HumanId).AsTask().Result;

        foreach (var item in listBox.Items)
        {
            if (item is Human hum && hum.Id == human.Id)
            {
                listBox.SelectedItem = item;
            }
        }
    }

    public ValueTask<string> AddEditCarAsync(UIElementCollection collection)
    {
        var valuesDict = new Dictionary<string, string>(CreateValuesRelationDict(collection));

        if (int.TryParse(valuesDict["price"], out var price) == false)
        {
            return new ValueTask<string>("В поле стоимость автомобиля введено не число.");
        }

        if (int.TryParse(valuesDict["year"], out var year) == false)
        {
            return new ValueTask<string>("В поле год выпуска автомобиля введено не число.");
        }

        if (int.TryParse(valuesDict["displacement"], out var displacement) == false)
        {
            return new ValueTask<string>("В поле рабочий объем двигателя автомобиля введено не число.");
        }

        if (DateTime.TryParse(valuesDict["issuingDate"], out var issuingDate) == false)
        {
            return new ValueTask<string>("В поле дата выдачи паспорта автомобиля введена не дата.");
        }
        
        var car = new Car
        {
            Brand = valuesDict["brand"],
            Model = valuesDict["model"],
            PassportNumber = valuesDict["passportNumber"],
            VIN = valuesDict["vin"],
            BodyNumber = valuesDict["bodyNumber"],
            Color = valuesDict["color"],
            EngineNumber = valuesDict["engineNumber"],
            RegistrationNumber = valuesDict["registrationNumber"],
            Price = price,
            Year = year,
            EngineDisplacement = displacement,
            PassportIssuingDate = issuingDate,
            WheelsType = _wheelsType[valuesDict["wheelsType"]],
            CarStatus = _status[valuesDict["status"]]
        };

        int.TryParse(valuesDict["owner"], out var humanId);
        var owners = BaseCommands<Owner>.SelectAllAsync().AsTask().Result;
        car.OwnerId = owners.Where(owner => owner.HumanId == humanId).FirstOrDefault().Id;

        var validationResults = ModelValidation.Validate(Car);
            
        if (validationResults.Any())
        {
            return new ValueTask<string>(validationResults.First().ErrorMessage);
        }

        SaveItemInDbAsync(Car);
        
        return new ValueTask<string>(string.Empty);
    }

    protected override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
    {
        if (item is not Car car)
        {
            return new Dictionary<string, string>();
        }

        return new Dictionary<string, string>
        {
            { "brand", car.Brand },
            { "model", car.Model },
            { "passportNumber", car.PassportNumber },
            { "passportIssuingDate", car.PassportIssuingDateString },
            { "vin", car.VIN },
            { "bodyNumber", car.BodyNumber },
            { "color", car.Color },
            { "year", car.Year.ToString() },
            { "engineNumber", car.EngineNumber },
            { "price", car.Price.ToString() },
            { "engineDisplacement", car.EngineDisplacement.ToString() },
            { "registrationNumber", car.RegistrationNumber },
            { "wheels", car.WheelsTypeString },
            { "status", car.CarStatusString }
        };
    }
}