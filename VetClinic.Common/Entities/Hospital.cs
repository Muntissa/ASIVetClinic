using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public string Type { get; set; }
        public int Lenght { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public virtual List<PatientInformation> PatientInformation { get; set; } = new();
    }
}
