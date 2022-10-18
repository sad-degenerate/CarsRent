using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class EngineDisplacementAttribute : ValidationAttribute
    {
        private readonly int _maxReplacement;
        
        public EngineDisplacementAttribute(int maxReplacement)
        {
            _maxReplacement = maxReplacement;
        }
        
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value.ToString(), out var displacement) == false)
            {
                return false;
            }

            return displacement <= _maxReplacement && displacement > 0;
        }
    }
}