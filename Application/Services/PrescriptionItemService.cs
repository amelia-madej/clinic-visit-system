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
    public class PrescriptionItemService : IPrescriptionItemService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<PrescriptionItemCreateDto> _createValidator;

        public PrescriptionItemService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<PrescriptionItemCreateDto> createValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public int Create(PrescriptionItemCreateDto dto, int prescriptionId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (prescriptionId <= 0)
                throw new ArgumentException("Invalid prescription ID", nameof(prescriptionId));

            _createValidator.ValidateAndThrow(dto);

            var prescription = _uow.PrescriptionRepository.Get(prescriptionId);
            if (prescription == null)
                throw new Exception("Prescription not found");

            var medication = _uow.MedicationRepository.Get(dto.MedicationId);
            if (medication == null)
                throw new Exception("Medication not found");

            var item = new PrescriptionItem
            {
                PrescriptionId = prescriptionId,
                MedicationId = dto.MedicationId,
                Dosage = dto.Dosage,
                Quantity = dto.Quantity,
                Instructions = dto.Instructions
            };

            _uow.PrescriptionItemRepository.Insert(item);
            _uow.Commit();

            return item.PrescriptionItemId;
        }

        public void Update(int prescriptionItemId, PrescriptionItemCreateDto dto)
        {
            if (prescriptionItemId <= 0)
                throw new ArgumentException("Invalid prescription item ID", nameof(prescriptionItemId));

            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            var item = _uow.PrescriptionItemRepository.Get(prescriptionItemId);
            if (item == null)
                throw new Exception("Prescription item not found");

            var medication = _uow.MedicationRepository.Get(dto.MedicationId);
            if (medication == null)
                throw new Exception("Medication not found");

            item.MedicationId = dto.MedicationId;
            item.Dosage = dto.Dosage;
            item.Quantity = dto.Quantity;
            item.Instructions = dto.Instructions;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid prescription item ID", nameof(id));

            var item = _uow.PrescriptionItemRepository.Get(id);
            if (item == null)
                throw new Exception("Prescription item not found");

            _uow.PrescriptionItemRepository.Delete(item);
            _uow.Commit();
        }

        public List<PrescriptionItemDto> GetAll()
        {
            var items = _uow.PrescriptionItemRepository.GetAllPrescriptionItems();
            return _mapper.Map<List<PrescriptionItemDto>>(items);
        }

        public PrescriptionItemDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid prescription item ID", nameof(id));

            var item = _uow.PrescriptionItemRepository.GetPrescriptionItemById(id);
            return item == null ? null : _mapper.Map<PrescriptionItemDto>(item);
        }

        public List<PrescriptionItemDto> GetByPrescriptionId(int prescriptionId)
        {
            if (prescriptionId <= 0)
                throw new ArgumentException("Invalid prescription ID", nameof(prescriptionId));

            var items = _uow.PrescriptionItemRepository.Find(pi => pi.PrescriptionId == prescriptionId).ToList();
            return _mapper.Map<List<PrescriptionItemDto>>(items);
        }
    }
}
