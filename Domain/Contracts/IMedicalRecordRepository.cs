using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IMedicalRecordRepository : IRepository<MedicalRecord>
    {
        MedicalRecord? GetMedicalRecordById(int id);
        List<MedicalRecord> GetAllMedicalRecords();
        MedicalRecord? GetMedicalRecordByVisitId(int visitId);
    }
}
