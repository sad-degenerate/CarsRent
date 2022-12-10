using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarsRent.LIB.Attributes;

public class RegNumberAttribute : ValidationAttribute
{
    private const string Pattern = @"[АВЕКМНОРСТУХ]\d{3}[АВЕКМНОРСТУХ]{2}\d{2,3}$";
    
    public override bool IsValid(object? value)
    {
        var str = value?.ToString();
        return string.IsNullOrWhiteSpace(str) == false && Regex.IsMatch(str, Pattern, RegexOptions.IgnoreCase);
    }
}