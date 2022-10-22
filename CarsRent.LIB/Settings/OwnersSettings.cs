using CarsRent.LIB.DataBase;
using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Settings
{
    public static class OwnersSettings
    {
        public static List<Human?> GetOwners(string searchText, int startPoint, int count)
        {
            var owners = string.IsNullOrWhiteSpace(searchText) 
                ? Commands<Owner>.SelectGroup(startPoint, count).ToList()
                : Commands<Owner>.FindAndSelect(searchText, startPoint, count).ToList();

            if (owners.Any())
            {
                return owners.Select(owner => Commands<Human>.SelectById(owner.HumanId)).ToList();
            }
            
            Default();
            return GetOwners(searchText, startPoint, count);
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

            Commands<Human>.Add(human);
            Commands<Owner>.Add(owner);

            return string.Empty;
        }

        public static string DeleteOwner(object selectedItem)
        {
            if (selectedItem is not Human human)
            {
                return "Вы не выбрали в списке арендодателя.";
            }
            
            if (Commands<Owner>.SelectGroup(0, 2).Count() < 2)
            {
                return "Вы не можете удалить единственного арендодателя в списке.";
            }

            foreach (var owner in human.Owners)
            {
                Commands<Owner>.Delete(owner);
            }
            Commands<Human>.Delete(human);

            return string.Empty;
        }

        public static void Default()
        {
            var human = new Human
            {
                Surname = "Фамилия",
                Name = "Имя",
                Patronymic = "Отчество",
                PassportNumber = "3434343343",
                IssuingDate = DateTime.Now,
                PhoneNumber = "88888888888",
                RegistrationPlace = "г. Город ул. Улица д. 1 кв. 1",
                BirthDate = DateTime.Now,
                IssuingOrganization = "ОРГАНИЗАЦИЯ"
            };

            var owner = new Owner
            {
                Human = human
            };

            Commands<Human>.Add(human);
            Commands<Owner>.Add(owner);
        }
    }
}