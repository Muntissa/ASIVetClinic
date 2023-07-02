using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public int InventoryNumber { get; set; }

        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        public int Lenght { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<AnimalHospitalInfo> PatientInformations { get; set; } = new();
    }
}
