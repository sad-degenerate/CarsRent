using System.Collections.Generic;
using CarsRent.LIB.Model;

namespace CarsRent.WPF.ViewControllers;

public class FillingContractFieldsController : FillingFieldsController
{
    public override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
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
}