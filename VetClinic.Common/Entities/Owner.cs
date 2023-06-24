using Microsoft.AspNetCore.Identity;

namespace VetClinic.Common.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public bool OwnerSex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ViewDocument { get; set; }
        public int SerialNumber { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}
