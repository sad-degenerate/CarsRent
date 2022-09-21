using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }    

            DateTime date;
            try
            {
                DateTime.TryParse(value.ToString(), out date);
            }
            catch (Exception)
            {
                return false;
            }

            if (Math.Abs(DateTime.Now.Year - date.Year) > 100)
            {
                return false;
            }

            return true;
        }
    }
}