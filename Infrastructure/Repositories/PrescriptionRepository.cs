using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        private readonly ClinicDbContext _dbContext;
        public PrescriptionRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Prescription> GetPrescriptionByVisitId(int visitId)
        {
            return _dbContext.Prescriptions
                .Include(p => p.Items)
                .ThenInclude(p => p.Medication)
                .Where(p => p.MedicalRecord.VisitId == visitId).ToList();
        }

        public List<Prescription> GetPrescriptionsByDoctorId(int doctorId)
        {
            return _dbContext.Prescriptions
                .Include(p => p.Items)
                .ThenInclude(p => p.Medication)
                .Where(p => p.MedicalRecord.Visit.DoctorId == doctorId).ToList();
        }

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            return _dbContext.Prescriptions
                .Include(p => p.Items)
                .ThenInclude(p => p.Medication)
                .Where(p => p.MedicalRecord.Visit.PatientId == patientId).ToList();
        }
    }
}
