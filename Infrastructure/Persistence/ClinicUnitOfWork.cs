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
        public IPatientRepository PatientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IVisitRepository VisitRepository { get; }
        public IMedicalRecordRepository MedicalRecordRepository { get; }
        public IPrescriptionRepository PrescriptionRepository { get; }
        public IMedicationRepository MedicationRepository { get; }
        public IUserRepository UserRepository { get; }
        public IPrescriptionItemRepository PrescriptionItemRepository { get; }
        public ISickLeaveRepository SickLeaveRepository { get; }
        public ClinicUnitOfWork(ClinicDbContext dbContext, IPatientRepository patients, IDoctorRepository doctors, IVisitRepository visits, IMedicalRecordRepository medicalRecords, IPrescriptionRepository prescriptions, IMedicationRepository medications, IUserRepository user, IPrescriptionItemRepository prescriptionItems, ISickLeaveRepository sickLeaves)
        {
            _dbContext = dbContext;
            PatientRepository = patients;
            DoctorRepository = doctors;
            VisitRepository = visits;
            MedicalRecordRepository = medicalRecords;
            PrescriptionRepository = prescriptions;
            MedicationRepository = medications;
            UserRepository = user;
            PrescriptionItemRepository = prescriptionItems;
            SickLeaveRepository = sickLeaves;
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
