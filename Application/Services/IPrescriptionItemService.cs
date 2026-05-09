using SharedKernel.DTOs;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IPrescriptionItemService
    {
        int Create(PrescriptionItemCreateDto dto, int prescriptionId);
        void Update(int prescriptionItemId, PrescriptionItemCreateDto dto);
        void Delete(int id);

        List<PrescriptionItemDto> GetAll();
        PrescriptionItemDto? GetById(int id);
        List<PrescriptionItemDto> GetByPrescriptionId(int prescriptionId);
    }
}
