using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Model
{
    public class ContractDetails : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели сумму залога")]
        [Price(ErrorMessage = "Некорректная сумма залога")]
        public int Deposit { get; set; }
        [Required(ErrorMessage = "Вы не ввели стоимость поездки")]
        [Price(ErrorMessage = "Некорректная стоимость поездки")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Вы не выбрали тип поездки")]
        public RideType RideType { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату заключения договора")]
        [Date(ErrorMessage = "Некорректная дата заключения договора")]
        public string ConclusionDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату расторжения договора")]
        [Date(ErrorMessage = "Некорректная дата расторжения договора")]
        public string EndDate { get; set; }

        public int? CarId { get; set; }
        [Required(ErrorMessage = "Вы не выбрали автомобиль, используемый в этой поездке")]
        public Car Car { get; set; }

        public int? RenterId { get; set; }
        [Required(ErrorMessage = "Вы не выбрали арендатора, учавствующего в этой поездке")]
        public Human Renter { get; set; }

        public ContractDetails() { }
    }

    public enum RideType
    {
        InTheCity,
        OutsideTheCity
    }
}