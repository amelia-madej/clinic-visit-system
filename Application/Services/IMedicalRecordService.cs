using SharedKernel.DTOs;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IMedicalRecordService
    {
        void Update(MedicalRecordDto dto);
        void Delete(int id);

        List<MedicalRecordDto> GetAll();
        MedicalRecordDto? GetById(int id);
        MedicalRecordDto? GetByVisitId(int visitId);
    }
}
