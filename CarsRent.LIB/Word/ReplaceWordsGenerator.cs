using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsGenerator
    {
        private Dictionary<string, string> _words;

        public ReplaceWordsGenerator(TemplatesSettings settings, ContractDetails contract)
        {
            _words = new Dictionary<string, string>()
            {
                { "llsurname", settings.Surname },
                { "llname", settings.Name },
                { "llpatronymic", settings.Patronymic },
                { "llinitials", $"{settings.Name[0]}.{settings.Patronymic[0]}." },
                { "llpassportNumbers", settings.PassportNumber },
                { "llissuingOrganization", settings.IssuingOrganization },
                { "llissuingDate", settings.IssuingDate },
                { "llregistrationPlace", settings.RegistrationPlace },
                { "surname", contract.Renter.Surname },
                { "name", contract.Renter.Name },
                { "patronymic", contract.Renter.Patronymic },
                { "initials", $"{contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}." },
                { "passportNumbers", contract.Renter.IdentityNumber },
                { "issuingOrganization", contract.Renter.IssuingOrganization },
                { "issuingDate", contract.Renter.IssuingDate },
                { "registrationPlace", contract.Renter.RegistrationPlace },
                { "conclusionDate", contract.ConclusionDate },
                { "endDate", contract.EndDate },
                { "rideType", contract.RideTypeText },
                { "ridePrice", contract.Price },
                { "endTime", DateTime.Now.TimeOfDay.ToString() },
                { "carPassportIssuingDate", contract.Car.PassportIssuingDate },
                { "carPassport", contract.Car.PassportNumber },
                { "brand", contract.Car.Brand },
                { "year", contract.Car.Year },
                { "vin", contract.Car.VIN },
                { "bodyNumber", contract.Car.BodyNumber },
                { "color", contract.Car.Color },
                { "model", contract.Car.Model },
                { "regNumber", "empty now" },
                { "engineNumber", contract.Car.EngineNumber },
                { "price", contract.Car.Price },
                { "deposit", contract.Deposit },
                { "contractNumber", contract.Id.ToString() },
                { "date", contract.ConclusionDate },
            };
        }

        public Dictionary<string, string> GetWords()
        {
            return _words;
        }
    }
}