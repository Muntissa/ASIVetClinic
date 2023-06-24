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
        }
    }
}