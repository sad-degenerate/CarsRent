using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRent.LIB.Model
{
    public class ContractDetails : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели сумму залога")]
        [Price(ErrorMessage = "Некорректная сумма залога")]
        public string Deposit { get; set; }
        [Required(ErrorMessage = "Вы не ввели стоимость поездки")]
        [Price(ErrorMessage = "Некорректная стоимость поездки")]
        public string Price { get; set; }
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
        [Required(ErrorMessage = "Вы не выбрали арендатора, участвующего в этой поездке")]
        public Human Renter { get; set; }

        public ContractDetails() { }

        [NotMapped]
        public string RideTypeText 
        { 
            get 
            {
                if (RideType == RideType.InTheCity)
                    return "по городу";
                else
                    return "за городом";
            } 
        }

        public override string ToString()
        {          
            return $"{Deposit} {Price} {RideTypeText} {ConclusionDate} {EndDate} {Car} {Renter}";
        }
    }

    public enum RideType
    {
        InTheCity,
        OutsideTheCity
    }
}