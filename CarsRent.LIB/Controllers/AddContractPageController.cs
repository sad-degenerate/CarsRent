using System.Windows.Controls;
using CarsRent.LIB.Model;

namespace CarsRent.LIB.Controllers;

public class AddContractPageController : BaseAddEntityController
{
    public Contract? Contract;

    private readonly Dictionary<string, RideType> _rideType;
        
    public AddContractPageController(Contract contract)
    {
        _rideType = new Dictionary<string, RideType>
        {
            { "по городу", RideType.InTheCity },
            { "за городом", RideType.OutsideTheCity }
        };

        Contract = contract;
    }
    
    protected override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
    {
        if (item is not Contract contract)
        {
            return new Dictionary<string, string>();
        }

        return new Dictionary<string, string>
        {
            { "deposit", contract.Deposit.ToString() },
            { "price", contract.Price.ToString() },
            { "rideType", contract.RideTypeText },
            { "conclusionDate", contract.ConclusionDateString },
            { "endDate", contract.EndDateString },
            { "endTime", contract.EndTime.ToString("t") }
        };
    }

    public override ValueTask<string> AddEditEntityAsync(UIElementCollection collection)
    {
        throw new NotImplementedException();
    }
}