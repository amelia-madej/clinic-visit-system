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
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<MedicalRecordDto> _updateValidator;

        public MedicalRecordService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<MedicalRecordDto> updateValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _updateValidator = updateValidator;
        }

        public void Update(MedicalRecordDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _updateValidator.ValidateAndThrow(dto);

            var medicalRecord = _uow.MedicalRecordRepository.Get(dto.Id);
            if (medicalRecord == null)
                throw new Exception("Medical record not found");

            medicalRecord.Interview = dto.Interview;
            medicalRecord.Diagnosis = dto.Diagnosis;
            medicalRecord.Recommendations = dto.Recommendations;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid medical record ID", nameof(id));

            var medicalRecord = _uow.MedicalRecordRepository.Get(id);
            if (medicalRecord == null)
                throw new Exception("Medical record not found");

            _uow.MedicalRecordRepository.Delete(medicalRecord);
            _uow.Commit();
        }

        public List<MedicalRecordDto> GetAll()
        {
            var medicalRecords = _uow.MedicalRecordRepository.GetAll().ToList();
            return _mapper.Map<List<MedicalRecordDto>>(medicalRecords);
        }

        public MedicalRecordDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid medical record ID", nameof(id));

            var medicalRecord = _uow.MedicalRecordRepository.Get(id);
            return medicalRecord == null ? null : _mapper.Map<MedicalRecordDto>(medicalRecord);
        }

        public MedicalRecordDto? GetByVisitId(int visitId)
        {
            if (visitId <= 0)
                throw new ArgumentException("Invalid visit ID", nameof(visitId));

            var medicalRecords = _uow.MedicalRecordRepository.Find(mr => mr.VisitId == visitId);
            var medicalRecord = medicalRecords.FirstOrDefault();
            return medicalRecord == null ? null : _mapper.Map<MedicalRecordDto>(medicalRecord);
        }
    }
}
