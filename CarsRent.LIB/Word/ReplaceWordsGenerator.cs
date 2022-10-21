using CarsRent.LIB.Model;
using NumbersToText;

namespace CarsRent.LIB.Word
{
    public class ReplaceWordsGenerator
    {
        private readonly Dictionary<string, string> _words;

        public ReplaceWordsGenerator(Contract contract)
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
                { "llsurname", contract.Car.Owner.Human.Surname },
                { "llname", contract.Car.Owner.Human.Name },
                { "llpatronymic", contract.Car.Owner.Human.Patronymic },
                { "llinitials", $"{contract.Car.Owner.Human.Name[0]}." +
                                $"{contract.Car.Owner.Human.Patronymic[0]}." },
                { "llpassportNumbers", $"серия: {contract.Car.Owner.Human.PassportNumber[..4]} " +
                                       $"№: {contract.Car.Owner.Human.PassportNumber.Substring(4, 6)}" },
                { "llissuingOrganization", contract.Car.Owner.Human.IssuingOrganization },
                { "llissuingDate", contract.Car.Owner.Human.IssuingDate.ToString("dd.MM.yyyy") },
                { "llregistrationPlace", contract.Car.Owner.Human.RegistrationPlace },
                { "surname", contract.Renter.Human.Surname },
                { "name", contract.Renter.Human.Name },
                { "patronymic", contract.Renter.Human.Patronymic },
                { "initials", $"{contract.Renter.Human.Name[0]}.{contract.Renter.Human.Patronymic[0]}." },
                { "passportNumbers", $"серия: {contract.Renter.Human.PassportNumber[..4]} " +
                                     $"№: {contract.Renter.Human.PassportNumber.Substring(4, 6)}" },
                { "issuingOrganization", contract.Renter.Human.IssuingOrganization },
                { "issuingDate", contract.Renter.Human.IssuingDate.ToString("dd.MM.yyyy") },
                { "registrationPlace", contract.Renter.Human.RegistrationPlace },
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
                { "phone", contract.Renter.Human.PhoneNumber },
            };
        }

        public Dictionary<string, string> GetWords()
        {
            return _words;
        }
    }
}