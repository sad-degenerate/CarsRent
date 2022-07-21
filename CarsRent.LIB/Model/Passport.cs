using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsRent.LIB.Model
{
    public class Passport : IBaseModel
    {
        [Key]
        [ForeignKey("Human")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели серию/номер паспорта")]
        [PassportNumber(ErrorMessage = "Некорректно введены серия/номер паспорта")]
        public string IdentityNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели кем выдан паспорт")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длинна строки должна быть не меньше 3 и не больше 50 символов")]
        public string IssuingOrganization { get; set; }
        [Required(ErrorMessage = "Не введена дата выдачи паспорта")]
        [Date(ErrorMessage = "Некорректная дата выдачи паспорта")]
        public DateTime IssuingDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели место регистрации")] 
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть не меньше 3 и не больше 50 символов")]
        public string RegistrationPlace { get; set; }

        public Passport() { }
    }
}