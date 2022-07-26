using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Validation
{
    public static class ModelValidation
    {
        public static List<ValidationResult> Validate<T>(T obj)
        {
            var validationContext = new ValidationContext(obj); ;
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, results, true);
            return results;
        }
    }
}