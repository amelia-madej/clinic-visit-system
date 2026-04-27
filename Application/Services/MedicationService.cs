using AutoMapper;
using Domain.Contracts;
using FluentValidation;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MedicationService(IClinicUnitOfWork clinicUnitOfWork, IMapper mapper)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
        }
        public List<MedicationDto> GetAll()
        {
            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetAll());
        }

        public MedicationDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Medication ID", nameof(id));

            var medication = _uow.MedicationRepository.Get(id);

            return medication == null ? null : _mapper.Map<MedicationDto>(medication);
        }

        public List<MedicationDto> GetByName(string name)
        { 
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Medication name cannot be empty", nameof(name));

            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByName(name));
        }

        public List<MedicationDto> GetByForm(string form)
        {
            if(string.IsNullOrWhiteSpace(form))
                throw new ArgumentException("Medication form cannot be empty", nameof(form));
            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByForm(form));
        }

        public List<MedicationDto> GetByStrength(decimal strengthValue)
        {
            if(strengthValue <= 0)
                throw new ArgumentException("Strength value must be greater than zero", nameof(strengthValue));

            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByStrengthValue(strengthValue));
        }

        public List<MedicationDto> GetByActiveIngredients(List<string> activeIngredients)
        {
            if(activeIngredients == null || activeIngredients.Count == 0)
                throw new ArgumentException("Active ingredients list cannot be null or empty", nameof(activeIngredients));
            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByActiveIngredient(activeIngredients));
        }

        public List<MedicationDto> GetByDoctorId(int doctorId)
        {
            if(doctorId <= 0)
                throw new ArgumentException("Invalid Doctor ID", nameof(doctorId));

            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByDoctorId(doctorId));
        }

        public List<MedicationDto> GetByPatientId(int patientId)
        {
            if(patientId <= 0)
                throw new ArgumentException("Invalid Patient ID", nameof(patientId));
            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByPatientId(patientId));
        }

        public List<MedicationDto> GetByPrescriptionId(int prescriptionId)
        {
            if(prescriptionId <= 0)
                throw new ArgumentException("Invalid Prescription ID", nameof(prescriptionId));
            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByPrescriptionId(prescriptionId));
        }

        public List<MedicationDto> GetByVisitId(int visitId)
        {
            if(visitId <= 0)
                throw new ArgumentException("Invalid Visit ID", nameof(visitId));
            return _mapper.Map<List<MedicationDto>>(_uow.MedicationRepository.GetMedicationsByVisitId(visitId));
        }
    }
}
