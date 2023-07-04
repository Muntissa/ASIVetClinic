using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common.Entities;

namespace VetClinic.Common
{
    public class VetClinicContext : IdentityDbContext<Employee>
    {
        public DbSet<Animal> Animals => Set<Animal>();
        public DbSet<AnimalHospitalInfo> AnimalHospitalInfos => Set<AnimalHospitalInfo>();
        public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
        public DbSet<Drug> Drugs => Set<Drug>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Reception> Receptions => Set<Reception>();
        public DbSet<Service> Services => Set<Service>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=80.90.186.201;Port=5432;Database=AisVetClinicdb;Username=muntissa;Password=P@SSword1sr00t");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "6b7bf0ac-b815-455a-8908-8133983c9200",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                UserName = "Администратор",
                NormalizedUserName = "АДМИНИСТРАТОР",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "root"),
                SecurityStamp = string.Empty,
                ConcurrencyStamp = string.Empty,

                Surname = "",
                Name = "Администратор",
                Patronymic = "",
                Sex = true,
                DateOfBirth = new DateTime(2001, 1, 1),
                EmploymentDate = new DateTime(2001, 1, 1),
                Position = "Администратор",
                Qualification = ""
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "6b7bf0ac-b815-455a-8908-8133983c9200",
                UserId = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7"
            });
        }
    }
}