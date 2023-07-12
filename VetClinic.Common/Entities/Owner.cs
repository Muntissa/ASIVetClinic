using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        [Display(Name = "Номер")]
        public int SerialNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "Имя")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; } = string.Empty;

        [Display(Name = "Пол")]
        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(100)]
        [Display(Name = "Данные документов")]
        public string DocumentData { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Адрес электронной почты")]
        public string? Email { get; set; }

        public List<Animal> Animals { get; set; } = new();
    }
}
