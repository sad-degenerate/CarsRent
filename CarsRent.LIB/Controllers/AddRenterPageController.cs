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
    
    public void FillFields(ref UIElementCollection collection)
    {
        var valuesDict = CreateValuesRelationDict(_renter);
        
        FillFields(ref collection, valuesDict);
    }

    public override ValueTask<string> AddEditEntityAsync(UIElementCollection collection)
    {
        var valuesDict = new Dictionary<string, string>(CreateValuesRelationDict(collection));

        if (DateTime.TryParse(valuesDict["issuingDate"], out var issuingDate) == false)
        {
            return new ValueTask<string>("В поле дата выдачи паспорта введена не дата.");
        }
        
        if (DateTime.TryParse(valuesDict["birthDate"], out var birthDate) == false)
        {
            return new ValueTask<string>("В поле дата рождения введена не дата.");
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
            return new ValueTask<string>(validationResults.First().ErrorMessage);
        }

        SaveItemInDbAsync(_renter);
        
        return new ValueTask<string>(string.Empty);
    }

    protected override void SaveItemInDbAsync<Human>(Human renter)
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

    protected override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
    {
        var human = item as Human;
        
        return new Dictionary<string, string>
        {
            { "name", human.Name },
            { "surname", human.Surname },
            { "patronymic", human.Patronymic },
            { "birthDate", human.BirthDateString },
            { "passportNumber", human.PassportNumber },
            { "issuingDate", human.IssuingDateString },
            { "issuingOrganization", human.IssuingOrganization },
            { "phoneNumber", human.PhoneNumber },
            { "registrationPlace", human.RegistrationPlace }
        };
    }
}