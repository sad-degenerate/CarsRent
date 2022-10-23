using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;

namespace CarsRent.LIB.Controllers;

public class AddCarPageController
{
    public Car? _car { get; private set; }
    
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

        _car = car;
    }

    public void CreateComboBoxesValues(ref ComboBox wheelsType, ref ComboBox carStatus)
    {
        wheelsType.ItemsSource = _wheelsType.Keys;
        wheelsType.SelectedIndex = 0;
        carStatus.ItemsSource = _status.Keys;
        carStatus.SelectedIndex = 0;
    }

    public Dictionary<string, string> CreateCarValuesDict()
    {
        return new Dictionary<string, string>
        {
            { "brand", _car.Brand },
            { "model", _car.Model },
            { "passportNumber", _car.PassportNumber },
            { "passportIssuingDate", _car.PassportIssuingDateString },
            { "vin", _car.VIN },
            { "bodyNumber", _car.BodyNumber },
            { "color", _car.Color },
            { "year", _car.Year.ToString() },
            { "engineNumber", _car.EngineNumber },
            { "price", _car.Price.ToString() },
            { "engineDisplacement", _car.EngineDisplacement.ToString() },
            { "registrationNumber", _car.RegistrationNumber },
            { "wheels", _car.WheelsTypeString },
            { "status", _car.CarStatusString }
        };
    }

    public ValueTask<List<Human>> GetOwners(string searchText, int startPoint, int count)
    {
        // if (string.IsNullOrWhiteSpace(searchText) == false)
        // {
        //     return from owner in BaseCommands<Owner>.FindAndSelect(searchText, startPoint, count) select owner.Human;
        // }
        //
        // if (_car == null || _car.OwnerId.HasValue == false)
        // {
        //     return from owner in BaseCommands<Owner>.SelectGroup(startPoint, count) select owner.Human;
        // }
        //
        // var list = new List<Human>
        // {
        //     BaseCommands<Owner>.SelectById(_car.OwnerId).Human
        // };
        // list.AddRange(BaseCommands<Owner>.SelectGroup(startPoint, count)
        //     .Where(x => x.Id != _car.OwnerId).Select(x => x.Human).ToList());
        //
        // return list;

        return HumanCommands.SelectAllOwnersAsync();
    }
}