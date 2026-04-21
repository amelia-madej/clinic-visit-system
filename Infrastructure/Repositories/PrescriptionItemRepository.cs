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
    public class PrescriptionItemRepository : Repository<PrescriptionItem>, IPrescriptionItemRepository
    {
        private readonly ClinicDbContext _dbContext;
        public PrescriptionItemRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PrescriptionItem> GetPrescriptionItemsByPrescriptionId(int prescriptionId)
        {
            return _dbContext.PrescriptionItems.Where(pi => pi.PrescriptionId == prescriptionId).ToList();
        }
    }
}
