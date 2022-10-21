using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class PassportNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            return (value.ToString() ?? string.Empty).Where(char.IsDigit).ToArray().Length == 10;
        }
    }
}