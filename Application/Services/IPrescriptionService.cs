using SharedKernel.DTOs;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IPrescriptionService
    {
        int Create(PrescriptionCreateDto dto, int medicalRecordId);
        void Update(int prescriptionId, PrescriptionCreateDto dto);
        void Delete(int id);

        List<PrescriptionListItemDto> GetAll();
        PrescriptionDetailsDto? GetById(int id);
        List<PrescriptionListItemDto> GetByMedicalRecordId(int medicalRecordId);
        List<PrescriptionListItemDto> GetExpired();
        List<PrescriptionListItemDto> GetExpiringSoon(int daysThreshold = 7);
    }
}
