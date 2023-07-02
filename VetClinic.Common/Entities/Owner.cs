using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Surname { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Patronymic { get; set; }

        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string DocumentData { get; set; }
        public int SerialNumber { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}
