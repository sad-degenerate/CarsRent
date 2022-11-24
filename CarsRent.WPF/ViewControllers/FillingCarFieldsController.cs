using System.Collections.Generic;
using CarsRent.LIB.Model;

namespace CarsRent.WPF.ViewControllers;

public class FillingCarFieldsController : FillingFieldsController
{
    public override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
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
            { "wheelsType", ((int)car.WheelsType).ToString() },
            { "status", ((int)car.CarStatus).ToString() }
        };
    }
}