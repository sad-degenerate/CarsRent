using System.Collections.Generic;
using CarsRent.LIB.Model;

namespace CarsRent.WPF.ViewControllers;

public class FillingRenterFieldsController : FillingFieldsController
{
    public override Dictionary<string, string> CreateValuesRelationDict(IBaseModel item)
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