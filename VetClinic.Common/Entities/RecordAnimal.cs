using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class RecordAnimal
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public float Weight { get; set; }

        public int? OwnerId { get; set; }
        public virtual Owner? Owner { get; set; }

        public virtual List<PatientInformation> PatientInformation { get; set; } = new();
    }
}
