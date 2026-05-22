using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        List<Prescription> GetAllWithDetails();
        Prescription? GetPrescriptionById(int id);
        List<Prescription> GetPrescriptionsByPatientId(int patientId);
        List<Prescription> GetPrescriptionsByDoctorId(int doctorId);
        List<Prescription> GetPrescriptionByVisitId(int visitId);

    }
}
