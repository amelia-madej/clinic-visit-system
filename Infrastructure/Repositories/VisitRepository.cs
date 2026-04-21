using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private readonly ClinicDbContext _dbContext;
        public VisitRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Visit> GetVisitsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                .Include(v => v.Doctor)
                .Include(v => v.MedicalRecord)
                .ThenInclude(v => v.Prescriptions)
                .Where(v => v.VisitDateTime >= startDate && v.VisitDateTime <= endDate).ToList();
        }

        public List<Visit> GetVisitsByDoctorId(int doctorId)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                .Include(v => v.Doctor)
                .Include(v => v.MedicalRecord)
                .ThenInclude(v => v.Prescriptions)
                .Where(v => v.DoctorId == doctorId).ToList();
        }

        public List<Visit> GetVisitsByPatientId(int patientId)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                .Include(v => v.Doctor)
                .Include(v => v.MedicalRecord)
                .ThenInclude(v => v.Prescriptions)
                .Where(v => v.PatientId == patientId).ToList();
        }
    }
}
