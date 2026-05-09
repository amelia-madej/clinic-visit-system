using Domain.Contracts;
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

        public Visit? GetVisitById(int id)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.User)
                .Include(v => v.Doctor)
                    .ThenInclude(d => d.User)
                .Include(v => v.MedicalRecord)
                    .ThenInclude(m => m.Prescriptions)
                        .ThenInclude(p => p.Items)
                            .ThenInclude(i => i.Medication)
                .Include(v => v.MedicalRecord)
                    .ThenInclude(m => m.SickLeave)
                .FirstOrDefault(v => v.VisitId == id);
        }

        public List<Visit> GetAllVisits()
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.User)
                .Include(v => v.Doctor)
                    .ThenInclude(d => d.User)
                .Include(v => v.MedicalRecord)
                    .ThenInclude(m => m.Prescriptions)
                        .ThenInclude(pi => pi.Items)
                .ToList();
        }

        public List<Visit> GetVisitsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.User)
                .Include(v => v.Doctor)
                    .ThenInclude(d => d.User)
                .Include(v => v.MedicalRecord)
                    .ThenInclude(v => v.Prescriptions)
                        .ThenInclude(pi => pi.Items)
                .Where(v => v.VisitDateTime >= startDate && v.VisitDateTime <= endDate).ToList();
        }

        public List<Visit> GetVisitsByDoctorId(int doctorId)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.User)
                .Include(v => v.Doctor)
                    .ThenInclude(d => d.User)
                .Include(v => v.MedicalRecord)
                    .ThenInclude(v => v.Prescriptions)
                .Where(v => v.DoctorId == doctorId).ToList();
        }

        public List<Visit> GetVisitsByPatientId(int patientId)
        {
            return _dbContext.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.User)
                .Include(v => v.Doctor)
                    .ThenInclude(d => d.User)
                .Include(v => v.MedicalRecord)
                    .ThenInclude(v => v.Prescriptions)
                .Where(v => v.PatientId == patientId).ToList();
        }
    }
}
