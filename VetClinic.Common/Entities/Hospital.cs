using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Hospital
    {
        public int Id { get; set; }

        [Display(Name = "Номер")]
        public int InventoryNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "Тип")]
        public string Type { get; set; } = string.Empty;

        [Display(Name = "Длина")]
        public int Lenght { get; set; }

        [Display(Name = "Ширина")]
        public int Width { get; set; }

        [Display(Name = "Высота")]
        public int Height { get; set; }

        public List<AnimalHospitalInfo> AnimalHospitalInfos { get; set; } = new();
    }
}
