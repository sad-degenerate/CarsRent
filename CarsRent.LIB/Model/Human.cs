﻿using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRent.LIB.Model
{
    public class Human : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели имя")]
        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Длина имени не должна быть меньше 1 символа и превышать 50 символов.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Вы не ввели фамилию")]
        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Длина фамилии не должна быть меньше 1 символа и превышать 50 символов.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Вы не ввели отчество")]
        [StringLength(50, MinimumLength = 1, 
            ErrorMessage = "Длина отчества не должна быть меньше 1 символа и превышать 50 символов.")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату рождения")]
        [Date(100, ErrorMessage = "Некорректная дата рождения")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели номер телефона")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели серию/номер паспорта")]
        [PassportNumber(ErrorMessage = "Некорректно введены серия/номер паспорта")]
        public string PassportNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели кем выдан паспорт")]
        [StringLength(200, MinimumLength = 3, 
            ErrorMessage = "Длинна строки \"кем выдан паспорт\" должна быть не меньше 3 и не больше 200 символов")]
        public string IssuingOrganization { get; set; }
        [Required(ErrorMessage = "Не введена дата выдачи паспорта")]
        [Date(100, ErrorMessage = "Некорректная дата выдачи паспорта")]
        public DateTime IssuingDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели место регистрации")]
        [StringLength(200, MinimumLength = 3, 
            ErrorMessage = "Длина строки \"место регистрации\" должна быть не меньше 3 и не больше 200 символов")]
        public string RegistrationPlace { get; set; }

        public virtual ICollection<Contract> ContractDetails { get; set; }
        public virtual ICollection<Renter> Renters { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }

        public Human() { }
        
        [NotMapped]
        public string BirthDateString => BirthDate.ToString("dd.MM.yyyy");

        [NotMapped]
        public string IssuingDateString => IssuingDate.ToString("dd.MM.yyyy");
        
        [NotMapped]
        public string FullName => $"{Surname} {Name[0]}.{Patronymic[0]}.";

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic} {BirthDateString} {PhoneNumber} {PassportNumber} " +
                   $"{IssuingOrganization} {IssuingDateString} {RegistrationPlace}";
        }
    }
}