using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class Owner
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string OwnerFloor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ViewDocument { get; set; }
        public int SerialNumber { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        
    }
}
