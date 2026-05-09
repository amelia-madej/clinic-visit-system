using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IVisitRepository : IRepository<Visit>
    {
        List<Visit> GetVisitsByPatientId(int patientId);
        List<Visit> GetVisitsByDoctorId(int doctorId);
        List<Visit> GetVisitsByDateRange(DateTime startDate, DateTime endDate);
        Visit? GetVisitById(int id);
        List<Visit> GetAllVisits();
    }
}
