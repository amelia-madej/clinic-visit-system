using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
    {
        private readonly ClinicDbContext _dbContext;
        public MedicalRecordRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
