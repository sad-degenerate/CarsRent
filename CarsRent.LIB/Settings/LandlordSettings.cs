using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Settings
{
    public class LandlordSettings : SettingsBase, IValidatableObject
    {
        public Human CurrentLandlord { get; set; }
        public List<Human> Landlords { get; set; }

        public LandlordSettings() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (Landlords.Count == 0)
            {
                errors.Add(new ValidationResult("Нет ни одного арендодателя."));
            }
            if (CurrentLandlord == null)
            {
                errors.Add(new ValidationResult("Не выбран текущий арендодатель."));
            }

            var landlorderrors = ModelValidation.Validate(CurrentLandlord);

            if (landlorderrors.Count > 0)
            {
                errors.AddRange(landlorderrors);
            }

            return errors;
        }
    }
}