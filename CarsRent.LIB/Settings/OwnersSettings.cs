using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Settings
{
    public static class OwnersSettings
    {
        public static ValueTask<List<Human>> GetOwners(string searchText, int startPoint, int count)
        {
            return string.IsNullOrWhiteSpace(searchText) 
                ? HumanCommands.SelectOwnersGroupAsync(startPoint, count) 
                : HumanCommands.FindAndSelectOwnersAsync(searchText, startPoint, count);
        }

        public static string AddOwner(Dictionary<string, string> fields)
        {
            var owner = new Owner();

            if (DateTime.TryParse(fields["birthDate"], out var birthDate) == false)
            {
                return "Ошибка при обработке даты рождения.";
            }

            if (DateTime.TryParse(fields["issuingDate"], out var issuingDate) == false)
            {
                return "Ошибка при обработке даты выдачи паспорта.";
            }

            var human = new Human
            {
                Surname = fields["surname"],
                Name = fields["name"],
                Patronymic = fields["patronymic"],
                PassportNumber = fields["passportNumber"],
                IssuingOrganization = fields["issuingOrganization"],
                RegistrationPlace = fields["registrationPlace"],
                PhoneNumber = fields["phone"],
                BirthDate = birthDate,
                IssuingDate = issuingDate
            };

            owner.Human = human;

            var humanResults = ModelValidation.Validate(human);

            if (humanResults.Any())
            {
                return humanResults.First().ErrorMessage;
            }

            BaseCommands<Human>.AddAsync(human);
            BaseCommands<Owner>.AddAsync(owner);

            return string.Empty;
        }

        public static string DeleteOwner(object selectedItem)
        {
            if (selectedItem is not Human human)
            {
                return "Вы не выбрали в списке арендодателя.";
            }
            
            if (HumanCommands.SelectOwnersGroupAsync(0, 2).AsTask().Result.Count < 2)
            {
                return "Вы не можете удалить единственного арендодателя в списке.";
            }

            foreach (var owner in human.Owners)
            {
                BaseCommands<Owner>.DeleteAsync(owner);
            }
            BaseCommands<Human>.DeleteAsync(human);

            return string.Empty;
        }
    }
}