using CarsRent.LIB.Model;
using CarsRent.LIB.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Settings
{
    [Serializable]
    public class TemplatesSettings : SettingsBase, IValidatableObject
    {
        public Human Landlord { get; set; }

        public string ActSample { get; set; }
        public string ContractSample { get; set; }
        public string NotificationSample { get; set; }
        public string OutputFolder { get; set; }

        public TemplatesSettings() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var landlordErrors = ModelValidation.Validate(Landlord);

            if (landlordErrors.Count > 0)
            {
                errors.AddRange(landlordErrors);
            }

            if (Path.GetExtension(ActSample) != ".docx")
            {
                errors.Add(new ValidationResult("Расширение не \".docx\"."));
            }
            if (Path.GetExtension(ContractSample) != ".docx")
            {
                errors.Add(new ValidationResult("Расширение не \".docx\"."));
            }
            if (Path.GetExtension(NotificationSample) != ".docx")
            {
                errors.Add(new ValidationResult("Расширение не \".docx\"."));
            }

            if (Directory.Exists(OutputFolder) == false)
            {
                try
                {
                    Directory.CreateDirectory(OutputFolder);
                }
                catch (Exception)
                {
                    errors.Add(new ValidationResult("Не удалось найти/создать директорию для вывода."));
                }
            }

            return errors;
        }
    }
}