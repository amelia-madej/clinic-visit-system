using Domain.Models;
using Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence
{
    public class ClinicDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<SickLeave> SickLeaves { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
            // Database.EnsureCreated() sprawdza, czy baza danych istnieje. 
            // Jeśli tak - nic nie robi. Jeśli nie - tworzy bazę i tabele zgodnie z modelem.
            // UWAGA: Gdy baza istnieje, nie jest sprawdzane, czy jest zgodna z modelem.
            // Aby zagwarantować zgodność bazy z modelem można rozważyć sekwecję instrukcji: 
            //      Database.EnsureDeleted();
            //      Database.EnsureCreated();
            // Powoduje to jednak zawsze usuwanie bazy przed rozpoczęciem dzialania programu.
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new VisitConfiguration());
            builder.ApplyConfiguration(new PatientConfiguration());
            builder.ApplyConfiguration(new DoctorConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PrescriptionConfiguration());
            builder.ApplyConfiguration(new PrescriptionItemConfiguration());
            builder.ApplyConfiguration(new MedicalRecordConfiguration());
            builder.ApplyConfiguration(new SickLeaveConfiguration());
            builder.ApplyConfiguration(new MedicationConfiguration());
        }
    }
}
