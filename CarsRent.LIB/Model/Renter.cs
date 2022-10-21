namespace CarsRent.LIB.Model;

public class Renter : IBaseModel
{
    public int Id { get; set; }

    public int? HumanId { get; set; }
    public virtual Human Human { get; set; }

    public Renter() { }

    public override string ToString()
    {
        return Human.ToString();
    }
}