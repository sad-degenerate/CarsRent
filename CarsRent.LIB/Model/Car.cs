using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CarsRent.LIB.Model
{
    public class Car : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо ввести марку автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна названия марки автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Необходимо ввести модель автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна названия модели автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Необходимо ввести номер паспорта автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна номера паспорта автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string PassportNumber { get; set; }
        [Required(ErrorMessage = "Необходимо ввести дату выдачи паспорта автомобиля")]
        [Date(ErrorMessage = "Некорректная дата выдачи паспорта автомобиля")]
        public DateTime PassportIssuingDate { get; set; }
        [Required(ErrorMessage = "Необходимо VIN автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна VIN автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string VIN { get; set; }
        [Required(ErrorMessage = "Необходимо ввести номер кузова автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна номера кузова автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string BodyNumber { get; set; }
        [Required(ErrorMessage = "Необходимо ввести цвет автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна названия цвета автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Необходимо год выпуска автомобиля")]
        [CarYearAttribute(ErrorMessage = "Некорректный годы выпуска автомобиля")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Необходимо ввести номер двигателя автомобиля")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длинна номера двигателя автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string EngineNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели стоимость автомобиля")]
        [Price(ErrorMessage = "Некорректная стоимость автомобиля")]
        public int Price { get; set; }

        public virtual ICollection<ContractDetails> ContractDetails { get; set; }

        public Car() { }
    }
}