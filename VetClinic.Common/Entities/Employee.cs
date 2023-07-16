using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Common.Entities
{
    public class Employee : IdentityUser
    {
        [MaxLength(100)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "Имя")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; } = string.Empty;

        [Display(Name = "Пол")]
        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата приема на работу")]
        public DateTime EmploymentDate { get; set; }

        [MaxLength(100)]
        [Display(Name = "Должность")]
        public string Position { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "Квалификация")]
        public string Qualification { get; set; } = string.Empty;

        [Display(Name = "Адрес электронной почты")]
        public override string? Email { get => base.Email; set => base.Email = value; }

        [Display(Name = "Номер телефона")]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [NotMapped]
        public string? Role { get; set; }
    }
}
