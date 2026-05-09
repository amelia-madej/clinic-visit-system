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
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<PrescriptionCreateDto> _createValidator;

        public PrescriptionService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<PrescriptionCreateDto> createValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public int Create(PrescriptionCreateDto dto, int medicalRecordId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (medicalRecordId <= 0)
                throw new ArgumentException("Invalid medical record ID", nameof(medicalRecordId));

            _createValidator.ValidateAndThrow(dto);

            var medicalRecord = _uow.MedicalRecordRepository.Get(medicalRecordId);
            if (medicalRecord == null)
                throw new Exception("Medical record not found");

            if (dto.Items != null)
            {
                foreach (var itemDto in dto.Items)
                {
                    if (_uow.MedicationRepository.Get(itemDto.MedicationId) == null)
                        throw new Exception($"Medication with ID {itemDto.MedicationId} not found");
                }
            }

            var prescription = new Prescription
            {
                MedicalRecordId = medicalRecordId,
                ValidUntil = dto.ValidUntil ?? DateTime.UtcNow.AddYears(1),
                CreatedAt = DateTime.UtcNow,
                Items = dto.Items?.Select(itemDto => new PrescriptionItem
                {
                    MedicationId = itemDto.MedicationId,
                    Dosage = itemDto.Dosage,
                    Quantity = itemDto.Quantity,
                    Instructions = itemDto.Instructions
                }).ToList() ?? new List<PrescriptionItem>()
            };

            _uow.PrescriptionRepository.Insert(prescription);
            _uow.Commit();

            return prescription.PrescriptionId;
        }

        public void Update(int prescriptionId, PrescriptionCreateDto dto)
        {
            if (prescriptionId <= 0)
                throw new ArgumentException("Invalid prescription ID", nameof(prescriptionId));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            var prescription = _uow.PrescriptionRepository.Get(prescriptionId);
            if (prescription == null)
                throw new Exception("Prescription not found");

            prescription.ValidUntil = dto.ValidUntil ?? DateTime.UtcNow.AddYears(1);

            _uow.Commit();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid prescription ID", nameof(id));

            var prescription = _uow.PrescriptionRepository.Get(id);
            if (prescription == null)
                throw new Exception("Prescription not found");

            _uow.PrescriptionRepository.Delete(prescription);
            _uow.Commit();
        }

        public List<PrescriptionListItemDto> GetAll()
        {
            var prescriptions = _uow.PrescriptionRepository.GetAll().ToList();
            return _mapper.Map<List<PrescriptionListItemDto>>(prescriptions);
        }

        public PrescriptionDetailsDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid prescription ID", nameof(id));

            var prescription = _uow.PrescriptionRepository.GetPrescriptionById(id);
            return prescription == null ? null : _mapper.Map<PrescriptionDetailsDto>(prescription);
        }

        public List<PrescriptionListItemDto> GetByMedicalRecordId(int medicalRecordId)
        {
            if (medicalRecordId <= 0)
                throw new ArgumentException("Invalid medical record ID", nameof(medicalRecordId));

            var prescriptions = _uow.PrescriptionRepository.Find(p => p.MedicalRecordId == medicalRecordId).ToList();
            return _mapper.Map<List<PrescriptionListItemDto>>(prescriptions);
        }

        public List<PrescriptionListItemDto> GetExpired()
        {
            var prescriptions = _uow.PrescriptionRepository.Find(p => p.ValidUntil < DateTime.UtcNow).ToList();
            return _mapper.Map<List<PrescriptionListItemDto>>(prescriptions);
        }

        public List<PrescriptionListItemDto> GetExpiringSoon(int daysThreshold = 7)
        {
            if (daysThreshold < 0)
                throw new ArgumentException("Days threshold must be non-negative", nameof(daysThreshold));

            var thresholdDate = DateTime.UtcNow.AddDays(daysThreshold);
            var prescriptions = _uow.PrescriptionRepository
                .Find(p => p.ValidUntil >= DateTime.UtcNow && p.ValidUntil <= thresholdDate)
                .ToList();

            return _mapper.Map<List<PrescriptionListItemDto>>(prescriptions);
        }
    }
}
