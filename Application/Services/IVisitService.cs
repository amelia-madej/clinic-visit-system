using SharedKernel.DTOs;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public interface IVisitService
    {
        int Create(VisitCreateDto dto);
        void Update(VisitUpdateDto dto);
        void Delete(int id);

        List<VisitListItemDto> GetAll();
        VisitDetailsDto? GetById(int id);
        
        List<VisitListItemDto> GetByPatientId(int patientId);
        List<VisitListItemDto> GetByDoctorId(int doctorId);
        List<VisitListItemDto> GetByDateRange(DateTime startDate, DateTime endDate);
        
        void CompleteVisit(int visitId, VisitCompleteDto dto);
    }
}
