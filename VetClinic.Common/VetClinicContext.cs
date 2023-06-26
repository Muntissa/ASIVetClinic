using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common.Entities;

namespace VetClinic.Common
{
    public class VetClinicContext : IdentityDbContext<MedicalStaff>
    {
        public DbSet<Diagnosis> Diagnosis => Set<Diagnosis>();
        public DbSet<Drugs> Drugs => Set<Drugs>();
        public DbSet<Hospital> Hospital => Set<Hospital>();
        public DbSet<MedicalStaff> MedicalStaff => Set<MedicalStaff>();
        public DbSet<Owner> Owner => Set<Owner>();
        public DbSet<PatientInformation> PatientInformation => Set<PatientInformation>();
        public DbSet<Reception> Reception => Set<Reception>();
        public DbSet<RecordAnimal> RecordAnimal => Set<RecordAnimal>();
        public DbSet<Services> Services => Set<Services>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientInformation>()
                .HasKey(bc => new { bc.RecordAnimalId, bc.HospitalId });

            modelBuilder.Entity<PatientInformation>()
                .HasOne(bc => bc.RecordAnimal)
                .WithMany(b => b.PatientInformation)
                .HasForeignKey(bc => bc.RecordAnimalId);

            modelBuilder.Entity<PatientInformation>()
                .HasOne(bc => bc.Hospital)
                .WithMany(c => c.PatientInformation)
                .HasForeignKey(bc => bc.HospitalId);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "6b7bf0ac-b815-455a-8908-8133983c9200",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<MedicalStaff>().HasData(new MedicalStaff
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
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "6b7bf0ac-b815-455a-8908-8133983c9200",
                UserId = "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7"
            });
        }
    }
}