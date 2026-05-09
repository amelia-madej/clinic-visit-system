using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IPrescriptionItemRepository : IRepository<PrescriptionItem>
    {
        PrescriptionItem? GetPrescriptionItemById(int id);
        List<PrescriptionItem> GetAllPrescriptionItems();
        List<PrescriptionItem> GetPrescriptionItemsByPrescriptionId(int prescriptionId);
    }
}
