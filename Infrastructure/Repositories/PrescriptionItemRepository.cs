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
    public class PrescriptionItemRepository : Repository<PrescriptionItem>, IPrescriptionItemRepository
    {
        private readonly ClinicDbContext _dbContext;
        public PrescriptionItemRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PrescriptionItem> GetPrescriptionItemsByPrescriptionId(int prescriptionId)
        {
            return _dbContext.PrescriptionItems
                .Where(pi => pi.PrescriptionId == prescriptionId)
                .Include(pi => pi.Medication)
                .ToList();
        }
    }
}
