using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class SickLeaveRepository : Repository<SickLeave>, ISickLeaveRepository
    {
        private readonly ClinicDbContext _dbContext;
        public SickLeaveRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SickLeave> GetSickLeavesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbContext.SickLeaves.Where(sl => sl.StartDate >= startDate && sl.EndDate <= endDate).ToList();
        }

        public List<SickLeave> GetSickLeavesByDoctorId(int doctorId)
        {
            return _dbContext.SickLeaves.Where(sl => sl.MedicalRecord.Visit.DoctorId == doctorId).ToList();
        }

        public List<SickLeave> GetSickLeavesByPatientId(int patientId)
        {
            return _dbContext.SickLeaves.Where(sl => sl.MedicalRecord.Visit.PatientId == patientId).ToList();
        }

        public List<SickLeave> GetSickLeavesByVisitId(int visitId)
        {
            return _dbContext.SickLeaves.Where(sl => sl.MedicalRecord.VisitId == visitId).ToList();
        }
    }
}
