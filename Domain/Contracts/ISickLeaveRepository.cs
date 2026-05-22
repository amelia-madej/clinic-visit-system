using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISickLeaveRepository : IRepository<SickLeave>
    {
        List<SickLeave> GetAllWithDetails();
        List<SickLeave> GetSickLeavesByPatientId(int patientId);
        List<SickLeave> GetSickLeavesByDoctorId(int doctorId);
        List<SickLeave> GetSickLeavesByDateRange(DateTime startDate, DateTime endDate);
        List<SickLeave> GetSickLeavesByVisitId(int visitId);
    }
}
