using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IClinicUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IPatientRepository PatientRepository { get; }
        IDoctorRepository DoctorRepository { get; }
        IMedicalRecordRepository MedicalRecordRepository { get; }
        IMedicationRepository MedicationRepository { get; }
        IPrescriptionItemRepository PrescriptionItemRepository { get; }
        IPrescriptionRepository PrescriptionRepository { get; }
        ISickLeaveRepository SickLeaveRepository { get; }
        IVisitRepository VisitRepository { get; }

        void Commit();
        void Dispose();
    }
}
