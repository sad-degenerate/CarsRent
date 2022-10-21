using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Settings
{
    [Serializable]
    public class TemplatesSettings : SettingsBase, IValidatableObject
    {
        public string ActSample { get; set; }
        public string ContractSample { get; set; }
        public string NotificationSample { get; set; }
        public string OutputFolder { get; set; }

        public TemplatesSettings() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

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
                Directory.CreateDirectory(OutputFolder);
            }

            return errors;
        }

        public override TemplatesSettings Default()
        {
            return new TemplatesSettings
            {
                ActSample = Environment.SpecialFolder.MyDocuments + "образец акта.docx",
                ContractSample = Environment.SpecialFolder.MyDocuments + "образец акта.docx",
                NotificationSample = Environment.SpecialFolder.MyDocuments + "образец акта.docx",
                OutputFolder = Environment.SpecialFolder.MyDocuments + "образец акта.docx"
            };
        }
    }
}