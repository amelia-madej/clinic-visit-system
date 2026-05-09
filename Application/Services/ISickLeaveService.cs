using SharedKernel.DTOs;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public interface ISickLeaveService
    {
        int Create(SickLeaveCreateDto dto);
        void Update(SickLeaveUpdateDto dto);
        void Delete(int id);

        List<SickLeaveListItemDto> GetAll();
        SickLeaveDetailsDto? GetById(int id);
        SickLeaveDetailsDto? GetByMedicalRecordId(int medicalRecordId);
        List<SickLeaveListItemDto> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}
