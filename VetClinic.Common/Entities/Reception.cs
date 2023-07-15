using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Reception
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Тип")]
        public string Type { get; set; } = string.Empty;

        [MaxLength(50)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public List<Diagnosis> Diagnoses { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<Drug> Drugs { get; set; } = new();

        [DataType(DataType.Currency)]
        [Precision(10, 2)]
        [Display(Name = "Сумма к оплате")]
        public decimal Price { get; set; }
    }
}
