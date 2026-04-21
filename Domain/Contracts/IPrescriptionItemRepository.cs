using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPrescriptionItemRepository : IRepository<PrescriptionItem>
    {
        List<PrescriptionItem> GetPrescriptionItemsByPrescriptionId(int prescriptionId);
    }
}
