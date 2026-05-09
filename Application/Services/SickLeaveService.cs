using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class SickLeaveService : ISickLeaveService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<SickLeaveCreateDto> _createValidator;
        private readonly IValidator<SickLeaveUpdateDto> _updateValidator;

        public SickLeaveService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<SickLeaveCreateDto> createValidator,
            IValidator<SickLeaveUpdateDto> updateValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public int Create(SickLeaveCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            var medicalRecord = _uow.MedicalRecordRepository.Get(dto.MedicalRecordId);
            if (medicalRecord == null)
                throw new Exception("Medical record not found");

            // Check if sick leave already exists for this medical record
            var existingSickLeave = _uow.SickLeaveRepository.Find(sl => sl.MedicalRecordId == dto.MedicalRecordId).FirstOrDefault();
            if (existingSickLeave != null)
                throw new Exception("Sick leave already exists for this medical record");

            var sickLeave = new SickLeave
            {
                MedicalRecordId = dto.MedicalRecordId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                CreatedAt = DateTime.UtcNow
            };

            _uow.SickLeaveRepository.Insert(sickLeave);
            _uow.Commit();

            return sickLeave.SickLeaveId;
        }

        public void Update(SickLeaveUpdateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _updateValidator.ValidateAndThrow(dto);

            var sickLeave = _uow.SickLeaveRepository.Get(dto.SickLeaveId);
            if (sickLeave == null)
                throw new Exception("Sick leave not found");

            sickLeave.StartDate = dto.StartDate;
            sickLeave.EndDate = dto.EndDate;
            sickLeave.Reason = dto.Reason;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid sick leave ID", nameof(id));

            var sickLeave = _uow.SickLeaveRepository.Get(id);
            if (sickLeave == null)
                throw new Exception("Sick leave not found");

            _uow.SickLeaveRepository.Delete(sickLeave);
            _uow.Commit();
        }

        public List<SickLeaveListItemDto> GetAll()
        {
            var sickLeaves = _uow.SickLeaveRepository.GetAll().ToList();
            return _mapper.Map<List<SickLeaveListItemDto>>(sickLeaves);
        }

        public SickLeaveDetailsDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid sick leave ID", nameof(id));

            var sickLeave = _uow.SickLeaveRepository.Get(id);
            return sickLeave == null ? null : _mapper.Map<SickLeaveDetailsDto>(sickLeave);
        }

        public SickLeaveDetailsDto? GetByMedicalRecordId(int medicalRecordId)
        {
            if (medicalRecordId <= 0)
                throw new ArgumentException("Invalid medical record ID", nameof(medicalRecordId));

            var sickLeave = _uow.SickLeaveRepository.Find(sl => sl.MedicalRecordId == medicalRecordId).FirstOrDefault();
            return sickLeave == null ? null : _mapper.Map<SickLeaveDetailsDto>(sickLeave);
        }

        public List<SickLeaveListItemDto> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date must be greater than or equal to start date");

            var sickLeaves = _uow.SickLeaveRepository
                .Find(sl => sl.StartDate <= endDate && sl.EndDate >= startDate)
                .ToList();

            return _mapper.Map<List<SickLeaveListItemDto>>(sickLeaves);
        }
    }
}
