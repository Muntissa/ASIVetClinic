using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Service
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Precision(10, 2)]
        public decimal Price { get; set; }

        public List<Reception> Receptions { get; set; } = new();
    }
}
