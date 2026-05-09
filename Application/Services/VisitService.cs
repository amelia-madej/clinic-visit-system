using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using SharedKernel;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class VisitService : IVisitService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<VisitCreateDto> _createValidator;
        private readonly IValidator<VisitUpdateDto> _updateValidator;
        private readonly IValidator<VisitCompleteDto> _completeValidator;

        public VisitService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<VisitCreateDto> createValidator,
            IValidator<VisitUpdateDto> updateValidator,
            IValidator<VisitCompleteDto> completeValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _completeValidator = completeValidator;
        }

        public int Create(VisitCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            var patient = _uow.PatientRepository.Get(dto.PatientId);
            if (patient == null)
                throw new Exception("Patient not found");

            var doctor = _uow.DoctorRepository.Get(dto.DoctorId);
            if (doctor == null)
                throw new Exception("Doctor not found");

            var visit = _mapper.Map<Visit>(dto);
            visit.Status = VisitStatus.Scheduled;
            visit.CreatedAt = DateTime.UtcNow;

            _uow.VisitRepository.Insert(visit);
            _uow.Commit();

            return visit.VisitId;
        }

        public void Update(VisitUpdateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _updateValidator.ValidateAndThrow(dto);

            var visit = _uow.VisitRepository.Get(dto.VisitId);
            if (visit == null)
                throw new Exception("Visit not found");

            visit.PatientId = dto.PatientId;
            visit.DoctorId = dto.DoctorId;
            visit.VisitDateTime = dto.VisitDateTime;
            visit.VisitType = Enum.Parse<VisitType>(dto.VisitType);

            if (!string.IsNullOrEmpty(dto.Status))
            {
                if (Enum.TryParse<VisitStatus>(dto.Status, out var status))
                {
                    visit.Status = status;
                }
            }

            _uow.Commit();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid visit ID", nameof(id));

            var visit = _uow.VisitRepository.Get(id);
            if (visit == null)
                throw new Exception("Visit not found");

            _uow.VisitRepository.Delete(visit);
            _uow.Commit();
        }

        public List<VisitListItemDto> GetAll()
        {
            var visits = _uow.VisitRepository.GetAllVisits();
            return _mapper.Map<List<VisitListItemDto>>(visits);
        }

        public VisitDetailsDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid visit ID", nameof(id));

            var visit = _uow.VisitRepository.GetVisitById(id);
            return visit == null ? null : _mapper.Map<VisitDetailsDto>(visit);
        }

        public List<VisitListItemDto> GetByPatientId(int patientId)
        {
            if (patientId <= 0)
                throw new ArgumentException("Invalid patient ID", nameof(patientId));

            var visits = _uow.VisitRepository.GetVisitsByPatientId(patientId);
            return _mapper.Map<List<VisitListItemDto>>(visits);
        }

        public List<VisitListItemDto> GetByDoctorId(int doctorId)
        {
            if (doctorId <= 0)
                throw new ArgumentException("Invalid doctor ID", nameof(doctorId));

            var visits = _uow.VisitRepository.GetVisitsByDoctorId(doctorId);
            return _mapper.Map<List<VisitListItemDto>>(visits);
        }

        public List<VisitListItemDto> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date must be greater than or equal to start date");

            var visits = _uow.VisitRepository.GetVisitsByDateRange(startDate, endDate);
            return _mapper.Map<List<VisitListItemDto>>(visits);
        }

        public void CompleteVisit(int visitId, VisitCompleteDto dto)
        {
            if (visitId <= 0)
                throw new ArgumentException("Invalid visit ID", nameof(visitId));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _completeValidator.ValidateAndThrow(dto);

            var visit = _uow.VisitRepository.Get(visitId);
            if (visit == null)
                throw new Exception("Visit not found");

            if (visit.Status == VisitStatus.Completed)
                throw new Exception("Visit is already completed");

            var medicalRecord = new MedicalRecord
            {
                VisitId = visitId,
                Interview = dto.Interview,
                Diagnosis = dto.Diagnosis,
                Recommendations = dto.Recommendations,
                CreatedAt = DateTime.UtcNow
            };

            _uow.MedicalRecordRepository.Insert(medicalRecord);
            _uow.Commit();

            visit.Status = VisitStatus.Completed;
            _uow.Commit();
        }
    }
}
