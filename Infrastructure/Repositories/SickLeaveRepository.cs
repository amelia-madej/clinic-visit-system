using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SickLeaveRepository : Repository<SickLeave>, ISickLeaveRepository
    {
        private readonly ClinicDbContext _dbContext;
        public SickLeaveRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SickLeave> GetAllWithDetails()
        {
            return _dbContext.SickLeaves
                .Include(sl => sl.MedicalRecord)
                    .ThenInclude(mr => mr!.Visit)
                        .ThenInclude(v => v!.Doctor)
                            .ThenInclude(d => d!.User)
                .Include(sl => sl.MedicalRecord)
                    .ThenInclude(mr => mr!.Visit)
                        .ThenInclude(v => v!.Patient)
                            .ThenInclude(p => p!.User)
                .ToList();
        }

        public List<SickLeave> GetSickLeavesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbContext.SickLeaves
                .Include(sl => sl.MedicalRecord)
                    .ThenInclude(mr => mr!.Visit)
                .Where(sl => sl.StartDate >= startDate && sl.EndDate <= endDate)
                .ToList();
        }

        public List<SickLeave> GetSickLeavesByDoctorId(int doctorId)
        {
            return _dbContext.SickLeaves
                .Include(sl => sl.MedicalRecord)
                    .ThenInclude(mr => mr!.Visit)
                .Where(sl => sl.MedicalRecord!.Visit!.DoctorId == doctorId)
                .ToList();
        }

        public List<SickLeave> GetSickLeavesByPatientId(int patientId)
        {
            return _dbContext.SickLeaves
                .Include(sl => sl.MedicalRecord)
                    .ThenInclude(mr => mr!.Visit)
                .Where(sl => sl.MedicalRecord!.Visit!.PatientId == patientId)
                .ToList();
        }

        public List<SickLeave> GetSickLeavesByVisitId(int visitId)
        {
            return _dbContext.SickLeaves
                .Include(sl => sl.MedicalRecord)
                .Where(sl => sl.MedicalRecord!.VisitId == visitId)
                .ToList();
        }
    }
}
