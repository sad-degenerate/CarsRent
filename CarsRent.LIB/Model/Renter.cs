using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRent.LIB.Model;

public class Renter : IBaseModel
{
    public int Id { get; set; }

    public int? HumanId { get; set; }
    public virtual Human Human { get; set; }

    public Renter() { }

    [NotMapped]
    public string FullName => Human.ToString();
}