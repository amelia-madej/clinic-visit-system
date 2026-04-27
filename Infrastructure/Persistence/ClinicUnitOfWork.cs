using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ClinicUnitOfWork : IClinicUnitOfWork
    {
        private readonly ClinicDbContext _dbContext;
        public IPatientRepository Patients { get; }
        public IDoctorRepository Doctors { get; }
        public IVisitRepository Visits { get; }
        public IMedicalRecordRepository MedicalRecords { get; }
        public IPrescriptionRepository Prescriptions { get; }
        public IMedicationRepository Medications { get; }
        public IUserRepository User { get; }
        public IPrescriptionItemRepository PrescriptionItems { get; }
        public ISickLeaveRepository SickLeaves { get; }
        public ClinicUnitOfWork(ClinicDbContext dbContext, IPatientRepository patients, IDoctorRepository doctors, IVisitRepository visits, IMedicalRecordRepository medicalRecords, IPrescriptionRepository prescriptions, IMedicationRepository medications, IUserRepository user, IPrescriptionItemRepository prescriptionItems, ISickLeaveRepository sickLeaves)
        {
            _dbContext = dbContext;
            Patients = patients;
            Doctors = doctors;
            Visits = visits;
            MedicalRecords = medicalRecords;
            Prescriptions = prescriptions;
            Medications = medications;
            User = user;
            PrescriptionItems = prescriptionItems;
            SickLeaves = sickLeaves;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
