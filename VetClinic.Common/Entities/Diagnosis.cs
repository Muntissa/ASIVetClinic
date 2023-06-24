using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Reception> ReceptionsList { get; set; } = new();
    }
}
