namespace CarsRent.LIB.Validation;

public static class DateValidation
{
    public static bool Validate(int year, int maxYear)
    {
        return year <= DateTime.Now.Year && year >= DateTime.Now.Year - maxYear;
    }
}