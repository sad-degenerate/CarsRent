using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CarsRent.LIB.Model
{
    public class Human : IBaseModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не ввели имя")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длина имени не должна быть меньше 1 символа и превышать 20 символов.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Вы не ввели фамилию")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длина фамилии не должна быть меньше 1 символа и превышать 20 символов.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Вы не ввели отчество")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длина отчества не должна быть меньше 1 символа и превышать 20 символов.")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Вы не ввели дату рождения")]
        [Date(ErrorMessage = "Некорректная дата рождения")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели номер телефона")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели серию/номер паспорта")]
        [PassportNumber(ErrorMessage = "Некорректно введены серия/номер паспорта")]
        public string IdentityNumber { get; set; }
        [Required(ErrorMessage = "Вы не ввели кем выдан паспорт")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длинна строки \"кем выдан паспорт\" должна быть не меньше 3 и не больше 50 символов")]
        public string IssuingOrganization { get; set; }
        [Required(ErrorMessage = "Не введена дата выдачи паспорта")]
        [Date(ErrorMessage = "Некорректная дата выдачи паспорта")]
        public string IssuingDate { get; set; }
        [Required(ErrorMessage = "Вы не ввели место регистрации")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки \"место регистрации\" должна быть не меньше 3 и не больше 50 символов")]
        public string RegistrationPlace { get; set; }

        [XmlIgnore]
        public virtual ICollection<ContractDetails> ContractDetails { get; set; }

        public Human() { }

        [NotMapped]
        public string FullName => $"{Surname} {Name} {Patronymic}";

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic} {BirthDate} {PhoneNumber} {IdentityNumber} {IssuingOrganization} {IssuingDate} {RegistrationPlace}";
        }
    }
}