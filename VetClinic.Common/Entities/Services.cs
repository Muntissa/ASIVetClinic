using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class Services
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }

        public virtual List<Reception> ReceptionsList { get; set; } = new();
    }
}
