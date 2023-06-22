using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Common.Entities
{
    public class Owner
    {
        public String Surname { get; set; }
        public String Name { get; set; }
        public String Patronymic { get; set; }
        public String OwnerFloor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String ViewDocument { get; set; }
        public int SerialNumber { get; set; }
        public int Phone { get; set; }
        public String Email { get; set; }
        
    }
}
