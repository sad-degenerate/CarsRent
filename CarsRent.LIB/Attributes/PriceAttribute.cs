using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class PriceAttribute : ValidationAttribute
    {
        private readonly int _maxPrice;

        public PriceAttribute(int maxPrice)
        {
            _maxPrice = maxPrice;
        }
        
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value.ToString(), out var price) == false)
            {
                return false;
            }

            return price <= _maxPrice && price > 0;
        }
    }
}