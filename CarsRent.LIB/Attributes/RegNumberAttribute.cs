using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarsRent.LIB.Attributes;

public class RegNumberAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var str = value?.ToString();
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        const string pattern = @"[АВЕКМНОРСТУХ]\d{3}[АВЕКМНОРСТУХ]{2}\d{2,3}$";

        return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
    }
}