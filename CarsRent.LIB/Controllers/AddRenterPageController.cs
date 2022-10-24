using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Controllers;

public class AddRenterPageController : BaseAddEntityController
{
    public Human? Renter { get; }

    public AddRenterPageController(Human? renter)
    {
        Renter = renter;
    }

    public ValueTask<string> AddEditRenterAsync(Human renter)
    {
        var validationResults = ModelValidation.Validate(renter);
            
        if (validationResults.Any())
        {
            return new ValueTask<string>(validationResults.First().ErrorMessage);
        }

        SaveRenterInDbAsync(renter);
        
        return new ValueTask<string>(string.Empty);
    }

    private void SaveRenterInDbAsync(Human renter)
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
        return new Dictionary<string, string>()
        {
            
        };
    }
}