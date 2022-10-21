using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Settings
{
    public class DisplaySettings : SettingsBase, IValidatableObject
    {
        public int TableOnePageElementsCount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (TableOnePageElementsCount <= 0)
            {
                validationResults.Add(new ValidationResult("Количество элементов на странице" +
                                                           " в таблице не может быть меньше либо равно 0"));
            }

            return validationResults;
        }

        public override DisplaySettings Default()
        {
            return new DisplaySettings()
            {
                TableOnePageElementsCount = 10
            };
        }
    }
}