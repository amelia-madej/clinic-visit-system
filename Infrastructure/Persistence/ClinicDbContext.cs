using Domain.Models;
using Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            // Database.EnsureCreated() checks whether the database exists and creates it if needed.
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
