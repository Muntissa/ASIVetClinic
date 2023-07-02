using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class MedicalStaff : IdentityUser
    {
        [MaxLength(100)]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Patronymic { get; set; } = string.Empty;

        public bool Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set; }

        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Qualification { get; set; } = string.Empty;
    }
}
