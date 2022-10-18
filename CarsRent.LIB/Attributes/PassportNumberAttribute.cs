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

            return value.ToString().Where(x => char.IsDigit(x)).ToArray().Length != 10;
        }
    }
}