using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Attributes
{
    public class PassportNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            var passportNumber = value.ToString();

            var numbersString = new string(passportNumber.Where(x => char.IsDigit(x)).ToArray());
            // TODO: Проверка онлайн

            if (numbersString.Length != 10)
                return false;

            return true;
        }
    }
}