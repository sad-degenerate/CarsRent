using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using NumbersToText;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsGenerator
    {
        private readonly Dictionary<string, string> _words;

        public ReplaceWordsGenerator(TemplatesSettings settings, ContractDetails contract)
        {
            var textFromNumbers = new TextFromNumbers();

            var carPrice = textFromNumbers.Make(contract.Car.Price);
            var deposit = textFromNumbers.Make(contract.Deposit);
            var ridePrice = textFromNumbers.Make(contract.Price);

            _words = new Dictionary<string, string>()
            {
                { "ridePriceString", ridePrice },
                { "priceString", carPrice },
                { "depositString", deposit },
                { "llsurname", settings.Landlord.Surname },
                { "llname", settings.Landlord.Name },
                { "llpatronymic", settings.Landlord.Patronymic },
                { "llinitials", $"{settings.Landlord.Name[0]}.{settings.Landlord.Patronymic[0]}." },
                { "llpassportNumbers", settings.Landlord.IdentityNumber },
                { "llissuingOrganization", settings.Landlord.IssuingOrganization },
                { "llissuingDate", settings.Landlord.IssuingDate.ToString("dd.MM.yyyy") },
                { "llregistrationPlace", settings.Landlord.RegistrationPlace },
                { "surname", contract.Renter.Surname },
                { "name", contract.Renter.Name },
                { "patronymic", contract.Renter.Patronymic },
                { "initials", $"{contract.Renter.Name[0]}.{contract.Renter.Patronymic[0]}." },
                { "passportNumbers", contract.Renter.IdentityNumber },
                { "issuingOrganization", contract.Renter.IssuingOrganization },
                { "issuingDate", contract.Renter.IssuingDate.ToString("dd.MM.yyyy") },
                { "registrationPlace", contract.Renter.RegistrationPlace },
                { "conclusionDate", contract.ConclusionDate.ToString("dd.MM.yyyy") },
                { "endDate", contract.EndDate.ToString("dd.MM.yyyy") },
                { "rideType", contract.RideTypeText },
                { "ridePrice", contract.Price.ToString() },
                { "endTime", contract.EndTime.ToString("HH:mm") },
                { "carPassportIssuingDate", contract.Car.PassportIssuingDate.ToString("dd.MM.yyyy") },
                { "carPassport", contract.Car.PassportNumber },
                { "brand", contract.Car.Brand },
                { "year", contract.Car.Year.ToString() },
                { "vinNum", contract.Car.VIN },
                { "bodyNumber", contract.Car.BodyNumber },
                { "color", contract.Car.Color },
                { "model", contract.Car.Model },
                { "regNumber", contract.Car.RegistrationNumber },
                { "engineNumber", contract.Car.EngineNumber },
                { "price", contract.Car.Price.ToString() },
                { "deposit", contract.Deposit.ToString() },
                { "contractNumber", contract.Id.ToString() },
                { "date", contract.ConclusionDate.ToString("dd.MM.yyyy") },
                { "wheelsType", contract.Car.WheelsTypeString },
                { "engineDisplacement", contract.Car.EngineDisplacement.ToString() },
                { "phone", contract.Renter.PhoneNumber },
            };
        }

        public Dictionary<string, string> GetWords()
        {
            return _words;
        }
    }
}