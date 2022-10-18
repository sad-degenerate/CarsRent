using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class DateAttribute : ValidationAttribute
    {
        private readonly int _maxAge;
        
        public DateAttribute(int maxAge)
        {
            _maxAge = maxAge;
        }
        
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value.ToString(), out var year) == false)
            {
                return false;
            }

            return year <= DateTime.Now.Year && year >= DateTime.Now.Year - _maxAge;
        }
    }
}