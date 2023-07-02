using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Animal
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        public bool Sex { get; set; }

        [MaxLength(100)]
        public string Breed { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Color { get; set; } = string.Empty;

        [Precision(10, 2)]
        public decimal Weight { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string? PhotoPath { get; set; }

        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public List<AnimalHospitalInfo> AnimalHospitalInfos { get; set; } = new();
    }
}
