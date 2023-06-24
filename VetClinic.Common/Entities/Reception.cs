using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class Reception
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string OfferDate { get; set; }
        public int Price { get; set; }

        public int MedicalStaffId { get; set; }
        public virtual MedicalStaff MedicalStaff { get; set; }

        public virtual List<Diagnosis> DiagnosisList { get; set; } = new();
        public virtual List<Services> ServicesList { get; set; } = new();
        public virtual List<Drugs> DrugsList { get; set; } = new();
    }
}
