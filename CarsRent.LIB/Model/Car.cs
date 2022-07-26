﻿using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRent.LIB.Model
{
    public class Car : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо ввести марку автомобиля")]
        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Длинна названия марки автомобиля, должна быть в диапазоне от 1 до 50 символов")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Необходимо ввести модель автомобиля")]
        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Длинна названия модели автомобиля, должна быть в диапазоне от 1 до 50 символов")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Необходимо ввести номер паспорта автомобиля")]
        [StringLength(20, MinimumLength = 1, 
            ErrorMessage = "Длинна номера паспорта автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string PassportNumber { get; set; }
        [Required(ErrorMessage = "Необходимо ввести дату выдачи паспорта автомобиля")]
        [Date(100, 
            ErrorMessage = "Некорректная дата выдачи паспорта автомобиля")]
        public DateTime PassportIssuingDate { get; set; }
        [Required(ErrorMessage = "Необходимо VIN автомобиля")]
        [StringLength(20, MinimumLength = 1, 
            ErrorMessage = "Длинна VIN автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string VIN { get; set; }
        [Required(ErrorMessage = "Необходимо ввести номер кузова автомобиля")]
        [StringLength(20, MinimumLength = 1, 
            ErrorMessage = "Длинна номера кузова автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string BodyNumber { get; set; }
        [Required(ErrorMessage = "Необходимо ввести цвет автомобиля")]
        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Длинна названия цвета автомобиля, должна быть в диапазоне от 1 до 50 символов")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Необходимо год выпуска автомобиля")]
        [Date(100, 
            ErrorMessage = "Некорректный год выпуска автомобиля")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Необходимо ввести номер двигателя автомобиля")]
        [StringLength(20, MinimumLength = 1, 
            ErrorMessage = "Длинна номера двигателя автомобиля, должна быть в диапазоне от 1 до 20 символов")]
        public string EngineNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели стоимость автомобиля")]
        [Price(1_000_000, 
            ErrorMessage = "Некорректная стоимость автомобиля")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Вы не ввели регистрационный номер")]
        [RegNumber(ErrorMessage = "Неверный формат регистрационного номера.")]
        public string RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Вы не выбрали тип шин")]
        public WheelsType WheelsType { get; set; }
        [Required(ErrorMessage = "Вы не ввели рабочий объем двигателя")]
        [EngineDisplacement(10_000, 
            ErrorMessage = "Некорректный рабочий объем двигателя")]
        public int EngineDisplacement { get; set; }
        [Required(ErrorMessage = "Вы не ввели статус автомобиля")]
        public Status CarStatus { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
        
        [Required(ErrorMessage = "Вы не выбрали хозяина автомобиля.")]
        public int? OwnerId { get; set; }
        public virtual Owner Owner { get; set; }

        public Car() { }

        [NotMapped]
        public string FullCarName => $"{Color} {Brand} {Model}";

        [NotMapped]
        public string WheelsTypeString => WheelsType == WheelsType.Summer ? "летние" : "зимние";

        [NotMapped]
        public string CarStatusString
        {
            get
            {
                return CarStatus switch
                {
                    Status.Ready => "готова",
                    Status.OnLease => "в аренде",
                    Status.UnderRepair => "в ремонте",
                    _ => "",
                };
            }
        }

        [NotMapped]
        public string PassportIssuingDateString => PassportIssuingDate.ToString("dd.MM.yyyy");

        public override string ToString()
        {
            return $"{Brand} {Model} {PassportNumber} {PassportIssuingDateString} " +
                   $"{VIN} {BodyNumber} {Color} {Year} {EngineNumber} {Price}";
        }
    }

    public enum WheelsType
    {
        Summer,
        Winter
    }

    public enum Status
    {
        Ready,
        OnLease,
        UnderRepair,
    }
}