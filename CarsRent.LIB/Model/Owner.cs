using CarsRent.LIB.DataBase;

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
        // TODO: При ближайшей возможности нужно убрать это дерьмо.
        return Commands<Human>.SelectById(HumanId).ToString();
    }
}