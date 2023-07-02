using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Reception
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        [Precision(10, 2)]
        public decimal Price { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public List<Diagnosis> Diagnoses { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<Drug> Drugs { get; set; } = new();
    }
}
