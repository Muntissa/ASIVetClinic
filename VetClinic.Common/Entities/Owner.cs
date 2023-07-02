using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }

        [MaxLength(100)]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Patronymic { get; set; } = string.Empty;

        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(100)]
        public string DocumentData { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
