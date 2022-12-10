using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRent.LIB.Model
{
    public class Contract : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели сумму залога")]
        [Price(50_000, ErrorMessage = "Некорректная сумма залога")]
        public int Deposit { get; set; }
        [Required(ErrorMessage = "Вы не ввели стоимость поездки")]
        [Price(10_000, ErrorMessage = "Некорректная стоимость поездки")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Вы не выбрали тип поездки")]
        public RideType RideType { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату заключения договора")]
        [Date(100, ErrorMessage = "Некорректная дата заключения договора")]
        public DateTime ConclusionDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату расторжения договора")]
        [Date(100, ErrorMessage = "Некорректная дата расторжения договора")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату окончания договора")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Вы не выбрали автомобиль, используемый в этой поездке")]
        public int? CarId { get; set; }
        public virtual Car Car { get; set; }

        [Required(ErrorMessage = "Вы не выбрали арендатора, участвующего в этой поездке")]
        public int? RenterId { get; set; }
        public virtual Renter Renter { get; set; }

        public Contract() { }

        [NotMapped]
        public string RideTypeText => RideType == RideType.InTheCity ? "по городу" : "за городом";

        [NotMapped]
        public string ConclusionDateString => ConclusionDate.ToString("dd.MM.yyyy");

        [NotMapped]
        public string EndDateString => EndDate.ToString("dd.MM.yyyy");

        public override string ToString()
        {          
            return $"{Deposit} {Price} {RideTypeText} {ConclusionDateString} {EndDateString} {Car} {Renter}";
        }
    }

    public enum RideType
    {
        InTheCity,
        OutsideTheCity
    }
}