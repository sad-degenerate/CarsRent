using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Controllers;

public class AddRenterPageController : BaseAddEntityController
{
    private Human? _renter;

    public AddRenterPageController(Human? renter)
    {
        _renter = renter;
    }

    public override string AddEditEntity(UIElementCollection collection, Dictionary<string, string> valuesRelDict)
    {
        var valuesDict = new Dictionary<string, string>(valuesRelDict);

        if (DateTime.TryParse(valuesDict["issuingDate"], out var issuingDate) == false)
        {
            return "В поле дата выдачи паспорта введена не дата.";
        }
        
        if (DateTime.TryParse(valuesDict["birthDate"], out var birthDate) == false)
        {
            return "В поле дата рождения введена не дата.";
        }

        _renter ??= new Human();

        _renter.Name = valuesDict["name"];
        _renter.Surname = valuesDict["surname"];
        _renter.Patronymic = valuesDict["patronymic"];
        _renter.BirthDate = birthDate;
        _renter.PassportNumber = valuesDict["passportNumber"];
        _renter.IssuingDate = issuingDate;
        _renter.IssuingOrganization = valuesDict["issuingOrganization"];
        _renter.PhoneNumber = valuesDict["phoneNumber"];
        _renter.RegistrationPlace = valuesDict["registrationPlace"];

        var validationResults = ModelValidation.Validate(_renter);
            
        if (validationResults.Any())
        {
            return validationResults.First().ErrorMessage;
        }

        SaveItemInDb(_renter);
        
        return string.Empty;
    }

    protected override void SaveItemInDb<Human>(Human renter)
    {
        if (renter.Id == 0)
        {
            BaseCommands<Human>.AddAsync(renter);
            BaseCommands<Renter>.AddAsync(new Renter
            {
                HumanId = renter.Id
            });
        }    
        else
        {
            BaseCommands<Human>.ModifyAsync(renter);
        }
    }
}