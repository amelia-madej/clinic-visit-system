using Domain.Models;
using Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistence
{
    public class ClinicDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Visit> Visit { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItem { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<SickLeave> SickLeave { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {
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
