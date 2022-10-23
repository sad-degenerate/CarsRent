namespace CarsRent.LIB.Model;

public class Owner : IBaseModel
{
    public int Id { get; set; }
    
    public int? HumanId { get; set; }
    public virtual Human Human { get; set; }
    
    public virtual ICollection<Car> OwnedCars { get; set; }

    public Owner() { }

    public override string ToString()
    {
        return Human.ToString();
    }
}