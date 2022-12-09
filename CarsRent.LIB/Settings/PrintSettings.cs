using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Settings;

public class PrintSettings : SettingsBase, IValidatableObject
{
    public int CopiesCount { get; set; }
    
    public bool TwoSidePrint { get; set; }
    
    public override SettingsBase Default()
    {
        return new PrintSettings()
        {
            CopiesCount = 2,
            TwoSidePrint = true
        };
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();

        if (CopiesCount <= 0)
        {
            validationResults.Add(new ValidationResult("Количество копий не может быть меньше либо равно нулю"));
        }

        return validationResults;
    }
}