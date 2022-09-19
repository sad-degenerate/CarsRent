using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class PriceAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int price;

            try
            {
                int.TryParse(value.ToString(), out price);
            }
            catch (Exception) 
            { 
                return false;
            }

            if (price <= 0 || price > 10000000)
            {
                return false;
            }

            return true;
        }
    }
}