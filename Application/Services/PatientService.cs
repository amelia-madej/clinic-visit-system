using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using FluentValidation;
using SharedKernel;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<PatientCreateDto> _createValidator;
        private readonly IValidator<PatientUpdateDto> _updateValidator;
        private readonly IPasswordHashService _passwordHashService;

        public PatientService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<PatientCreateDto> createValidator,
            IValidator<PatientUpdateDto> updateValidator,
            IPasswordHashService passwordHashService)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _passwordHashService = passwordHashService;
        }
        public int Create(PatientCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            if (_uow.PatientRepository.GetPatientByPesel(dto.Pesel) != null)
                throw new Exception("Patient with this PESEL already exists");

            if (_uow.PatientRepository.GetPatientByEmail(dto.Email) != null)
                throw new Exception("User with this email already exists");

            var user = _mapper.Map<User>(dto);
            user.Role = UserRole.Patient;
            user.Password = _passwordHashService.Hash(dto.Password);

            _uow.UserRepository.Insert(user);
            _uow.Commit(); 

            var patient = _mapper.Map<Patient>(dto);
            patient.UserId = user.UserId;

            _uow.PatientRepository.Insert(patient);
            _uow.Commit();

            return patient.PatientId;
        }

        public List<PatientListItemDto> GetAll()
        {
            var patients = _uow.PatientRepository.GetAllWithDetails();

            return _mapper.Map<List<PatientListItemDto>>(patients);
        }

        public PatientDetailsDto? GetById(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid patient ID", nameof(id));

            var patient = _uow.PatientRepository.GetByIdWithDetails(id);

            return patient == null ? null : _mapper.Map<PatientDetailsDto>(patient);
        }

        public PatientDetailsDto? GetByEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            var patient = _uow.PatientRepository.GetPatientByEmail(email);
            return patient == null ? null : _mapper.Map<PatientDetailsDto>(patient);
        }

        public PatientDetailsDto? GetByPesel(string pesel)
        {
            if(string.IsNullOrWhiteSpace(pesel))
                throw new ArgumentException("PESEL cannot be null or empty", nameof(pesel));

            var patient = _uow.PatientRepository.GetPatientByPesel(pesel);
            return patient == null ? null : _mapper.Map<PatientDetailsDto>(patient);
        }

        public PatientDetailsDto? GetByPhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

            var patient = _uow.PatientRepository.GetPatientByPhoneNumber(phoneNumber);
            return patient == null ? null : _mapper.Map<PatientDetailsDto>(patient);
        }

        public void Update(PatientUpdateDto dto)
        {
            _updateValidator.ValidateAndThrow(dto);

            var patient = _uow.PatientRepository.GetByIdWithDetails(dto.PatientId);

            if (patient == null)
                throw new Exception("Patient not found");

            patient.Pesel = dto.Pesel;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Address = dto.Address;
            patient.Gender = dto.Gender;

            patient.User.FirstName = dto.FirstName;
            patient.User.LastName = dto.LastName;
            patient.User.Email = dto.Email;
            patient.User.PhoneNumber = dto.PhoneNumber;

            _uow.Commit();
        }

        public void Delete(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid patient ID", nameof(id));

            var patient = _uow.PatientRepository.Get(id);

            if (patient == null)
                throw new Exception("Patient not found");

            var user = _uow.UserRepository.Get(patient.UserId);
            if(user == null)
                throw new Exception("Associated user not found");

            _uow.UserRepository.Delete(user);
            _uow.PatientRepository.Delete(patient);

            _uow.Commit();
        }
    }
}
