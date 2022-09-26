using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using NumbersToText;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsGenerator
    {
        private readonly Dictionary<string, string> _words;

        public ReplaceWordsGenerator(LandlordSettings settings, ContractDetails contract)
        {
            var textFromNumbers = new TextFromNumbers();

            var carPrice = textFromNumbers.Make(contract.Car.Price);
            var deposit = textFromNumbers.Make(contract.Deposit);
            var ridePrice = textFromNumbers.Make(contract.Price);

            _words = new Dictionary<string, string>()
            {
                { "carPassportIssuingDate", contract.Car.PassportIssuingDate.ToString("dd.MM.yyyy") },
                { "ridePriceString", ridePrice },
                { "priceString", carPrice },
                { "depositString", deposit },
                { "llsurname", settings.CurrentLandlord.Surname },
                { "llname", settings.CurrentLandlord.Name },
                { "llpatronymic", settings.CurrentLandlord.Patronymic },
                { "llinitials", $"{settings.CurrentLandlord.Name[0]}.{settings.CurrentLandlord.Patronymic[0]}." },
                { "llpassportNumbers", settings.CurrentLandlord.IdentityNumber },
                { "llissuingOrganization", settings.CurrentLandlord.IssuingOrganization },
                { "llissuingDate", settings.CurrentLandlord.IssuingDate.ToString("dd.MM.yyyy") },
                { "llregistrationPlace", settings.CurrentLandlord.RegistrationPlace },
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