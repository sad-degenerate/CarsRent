using System.Windows.Controls;
using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Controllers;

public class AddRenterPageController : BaseAddEntityController
{
    private Human? Renter { get; }

    public AddRenterPageController(Human? renter)
    {
        Renter = renter;
    }
    
    public void FillFields(ref UIElementCollection collection)
    {
        var valuesDict = CreateValuesRelationDict(Renter);
        
        base.FillFields(ref collection, valuesDict);
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
        
        var renter = new Human
        {
            Name = valuesDict["name"],
            Surname = valuesDict["surname"],
            Patronymic = valuesDict["patronymic"],
            BirthDate = birthDate,
            PassportNumber = valuesDict["passportNumber"],
            IssuingDate = issuingDate,
            IssuingOrganization = valuesDict["issuingOrganization"],
            PhoneNumber = valuesDict["phoneNumber"],
            RegistrationPlace = valuesDict["registrationPlace"]
        };

        var validationResults = ModelValidation.Validate(renter);
            
        if (validationResults.Any())
        {
            return new ValueTask<string>(validationResults.First().ErrorMessage);
        }

        SaveItemInDbAsync(renter);
        
        return new ValueTask<string>(string.Empty);
    }

    protected override void SaveItemInDbAsync<Human>(Human renter)
    {
        if (renter.Id == 0)
        {
            BaseCommands<Renter>.AddAsync(new Renter
            {
                HumanId = renter.Id
            });
            BaseCommands<Human>.AddAsync(renter);
        }    
        else
        {
            BaseCommands<Human>.ModifyAsync(renter);
        }
    }

    protected override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
    {
        if (item is not Human human)
        {
            return new Dictionary<string, string>();
        }
        
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