using System.ComponentModel.DataAnnotations;
using CarsRent.LIB.Validation;

namespace CarsRent.LIB.Attributes
{
    public class DateAttribute : ValidationAttribute
    {
        private readonly int _maxYear;
        
        public DateAttribute(int maxYear)
        {
            _maxYear = maxYear;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (DateTime.TryParse(value.ToString(), out var date))
            {
                return DateValidation.Validate(date.Year, _maxYear);
            }
            
            return int.TryParse(value.ToString(), out var year) && DateValidation.Validate(year, _maxYear);
        }
    }
}