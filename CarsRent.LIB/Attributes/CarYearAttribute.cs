using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class CarYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int year;

            try
            {
                int.TryParse(value.ToString(), out year);
            }
            catch (Exception)
            {
                return false;
            }

            if (year > DateTime.Now.Year || year < DateTime.Now.Year - 100)
            {
                return false;
            }

            return true;
        }
    }
}