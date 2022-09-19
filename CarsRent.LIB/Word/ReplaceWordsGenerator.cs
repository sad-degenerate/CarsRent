using CarsRent.LIB.Model;
using CarsRent.LIB.Settings;
using NumbersToText;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsGenerator
    {
        private Dictionary<string, string> _words;

        public ReplaceWordsGenerator(TemplatesSettings settings, ContractDetails contract)
        {
            var textFromNumbers = new TextFromNumbers();

            string carPriceString;
            if (long.TryParse(contract.Car.Price, out var carPrice))
                carPriceString = textFromNumbers.Make(carPrice);
            else
                carPriceString = string.Empty;

            string depositString;
            if (long.TryParse(contract.Deposit, out var deposit))
                depositString = textFromNumbers.Make(deposit);
            else
                depositString = string.Empty;

            string ridePriceString;
            if (long.TryParse(contract.Price, out var ridePrice))
                ridePriceString = textFromNumbers.Make(ridePrice);
            else
                ridePriceString = string.Empty;

            _words = new Dictionary<string, string>()
            {
                { "priceString", carPriceString },
                { "depositString", depositString },
                { "ridePriceString", ridePriceString },
                { "llsurname", settings.Landlord.Surname },
                { "llname", settings.Landlord.Name },
                { "llpatronymic", settings.Landlord.Patronymic },
                { "llinitials", $"{settings.Landlord.Name[0]}.{settings.Landlord.Patronymic[0]}." },
                { "llpassportNumbers", settings.Landlord.IdentityNumber },
                { "llissuingOrganization", settings.Landlord.IssuingOrganization },
                { "llissuingDate", settings.Landlord.IssuingDate },
                { "llregistrationPlace", settings.Landlord.RegistrationPlace },
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
                { "endTime", $"{DateTime.Now.TimeOfDay.Hours}:{DateTime.Now.TimeOfDay.Minutes}" },
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