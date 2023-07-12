using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        [Precision(10, 2)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public List<Reception> Receptions { get; set; } = new();
    }
}
