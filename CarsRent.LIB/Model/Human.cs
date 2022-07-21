using CarsRent.LIB.Attributes;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public virtual Passport Passport { get; set; }

        public virtual ICollection<ContractDetails> ContractDetails { get; set; }

        public Human() { }
    }
}