using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class EngineDiaplacementAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int displacement;

            try
            {
                int.TryParse(value.ToString(), out displacement);
            }
            catch (Exception)
            {
                return false;
            }

            if (displacement > 10000 || displacement < 0)
            {
                return false;
            }

            return true;
        }
    }
}