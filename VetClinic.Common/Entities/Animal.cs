using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Animal
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Display(Name = "Кличка")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Пол")]
        public bool Sex { get; set; }

        [MaxLength(100)]
        [Display(Name = "Порода")]
        public string Breed { get; set; } = string.Empty;

        [MaxLength(50)]
        [Display(Name = "Цвет")]
        public string Color { get; set; } = string.Empty;

        [Precision(10, 2)]
        [Display(Name = "Вес")]
        public decimal Weight { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения (примерная)")]
        public DateTime DateOfBirth { get; set; }

        public string? PhotoPath { get; set; }

        /*public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }*/

        public List<Owner> Owners { get; set; } = new();
        public List<AnimalHospitalInfo> AnimalHospitalInfos { get; set; } = new();
    }
}
