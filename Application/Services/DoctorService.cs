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
    public class DoctorService : IDoctorService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<DoctorCreateDto> _createValidator;
        private readonly IValidator<DoctorUpdateDto> _updateValidator;

        public DoctorService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<DoctorCreateDto> createValidator,
            IValidator<DoctorUpdateDto> updateValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public int Create(DoctorCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            if (_uow.DoctorRepository.GetDoctorByEmail(dto.Email) != null)
                throw new Exception("User with this email already exists");

            if (_uow.DoctorRepository.GetDoctorByPhoneNumber(dto.PhoneNumber) != null)
                throw new Exception("User with this phone number already exists");

            var user = _mapper.Map<User>(dto);
            user.Role = UserRole.Patient;

            _uow.UserRepository.Insert(user);
            _uow.Commit();

            var doctor = _mapper.Map<Doctor>(dto);
            doctor.UserId = user.UserId;

            _uow.DoctorRepository.Insert(doctor);
            _uow.Commit();

            return doctor.DoctorId;
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid doctor ID", nameof(id));

            var doctor = _uow.DoctorRepository.Get(id);

            if (doctor == null)
                throw new Exception("Doctor not found");

            _uow.UserRepository.Delete(doctor.User);
            _uow.DoctorRepository.Delete(doctor);

            _uow.Commit();
        }

        public List<DoctorListItemDto> GetAll()
        {
            var doctors = _uow.DoctorRepository.GetAll();

            return _mapper.Map<List<DoctorListItemDto>>(doctors);
        }

        public DoctorDetailsDto? GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid doctor ID", nameof(id));

            var doctor = _uow.DoctorRepository.GetByIdWithDetails(id);

            return doctor == null ? null : _mapper.Map<DoctorDetailsDto>(doctor);
        }

        public DoctorDetailsDto? GetDoctorsByLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            var doctor = _uow.DoctorRepository.GetDoctorsByLastName(lastName);
            return doctor == null ? null : _mapper.Map<DoctorDetailsDto>(doctor);
        }

        public DoctorDetailsDto? GetDoctorsBySpecialization(string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization name cannot be null or empty", nameof(specialization));

            var doctor = _uow.DoctorRepository.GetDoctorsBySpecialization(specialization);
            return doctor == null ? null : _mapper.Map<DoctorDetailsDto>(doctor);
        }

        public void Update(DoctorUpdateDto dto)
        {
            _updateValidator.ValidateAndThrow(dto);

            var doctor = _uow.DoctorRepository.GetByIdWithDetails(dto.DoctorId);

            if (doctor == null)
                throw new Exception("Doctor not found");

            doctor.Specialization = dto.Specialization;
            doctor.LicenseNumber = dto.LicenseNumber;
            doctor.Gender = dto.Gender;

            doctor.User.FirstName = dto.FirstName;
            doctor.User.LastName = dto.LastName;
            doctor.User.Email = dto.Email;
            doctor.User.PhoneNumber = dto.PhoneNumber;

            _uow.Commit();
        }
    }
}
