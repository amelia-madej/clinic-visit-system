using Domain.Contracts;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        private readonly ClinicDbContext _dbContext;
        public MedicalRecordRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public MedicalRecord? GetMedicalRecordById(int id) =>
            _dbContext.MedicalRecords
                .Include(m => m.Prescriptions)
                    .ThenInclude(p => p.Items)
                        .ThenInclude(i => i.Medication)
                .Include(m => m.SickLeave)
                .FirstOrDefault(m => m.MedicalRecordId == id);

        public List<MedicalRecord> GetAllMedicalRecords() =>
            _dbContext.MedicalRecords
                .Include(m => m.Prescriptions)
                    .ThenInclude(p => p.Items)
                        .ThenInclude(i => i.Medication)
                .Include(m => m.SickLeave)
                .ToList();

        public MedicalRecord? GetMedicalRecordByVisitId(int visitId) =>
            _dbContext.MedicalRecords
                .Include(m => m.Prescriptions)
                    .ThenInclude(p => p.Items)
                        .ThenInclude(i => i.Medication)
                .Include(m => m.SickLeave)
                .FirstOrDefault(m => m.VisitId == visitId);
    }
}
