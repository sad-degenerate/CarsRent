using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Settings
{
    public static class OwnersSettings
    {
        public static ValueTask<List<Human>> GetOwnersAsync(string searchText, int startPoint, int count)
        {
            var owners = BaseCommands<Owner>.SelectGroup(startPoint, count, searchText).ToList();
            var humans = (from owner in owners select owner.Human).ToList();

            return new ValueTask<List<Human>>(humans);
        }

        public static string AddOwner(Dictionary<string, string> fields, int? id)
        {
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
                PhoneNumber = fields["phoneNumber"],
                BirthDate = birthDate,
                IssuingDate = issuingDate
            };

            var humanResults = ModelValidation.Validate(human);

            if (humanResults.Any())
            {
                return humanResults.First().ErrorMessage;
            }

            if (id.HasValue)
            {
                var owner = BaseCommands<Owner>.SelectById(id);
                var humanId = owner.HumanId;
                owner.Human = human;
                owner.Human.Id = (int)humanId;
                
                BaseCommands<Human>.Modify(human);
                BaseCommands<Owner>.Modify(owner);

                return string.Empty;
            }
            
            BaseCommands<Human>.Add(human);
            BaseCommands<Owner>.Add(new Owner()
            {
                HumanId = human.Id
            });

            return string.Empty;
        }

        public static string DeleteOwner(object selectedItem)
        {
            if (selectedItem is not Human human)
            {
                return "Вы не выбрали в списке арендодателя.";
            }

            var owners = BaseCommands<Owner>.SelectAll()
                .Where(owner => owner.HumanId == human.Id);

            foreach (var owner in owners)
            {
                BaseCommands<Owner>.Delete(owner);
            }
            BaseCommands<Human>.Delete(human);

            return string.Empty;
        }
    }
}