using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Diagnosis
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        public List<Reception> Receptions { get; set; } = new();
    }
}
