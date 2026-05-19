using AutoMapper;
using Domain.Contracts;
using FluentValidation;
using SharedKernel.DTOs;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthService(IClinicUnitOfWork uow, IMapper mapper, IValidator<LoginDto> loginValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _loginValidator = loginValidator;
        }

        public AuthResponseDto Login(LoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _loginValidator.ValidateAndThrow(dto);

            var user = _uow.UserRepository.GetUserByEmail(dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password");

            // NA RAZIE plain text
            if (user.Password != dto.Password)
                throw new Exception("Invalid email or password");

            return new AuthResponseDto
            {
                UserId = user.UserId,
                PatientId = user.Patient?.PatientId ?? _uow.PatientRepository.GetPatientByEmail(user.Email)?.PatientId,
                DoctorId = user.Doctor?.DoctorId ?? _uow.DoctorRepository.GetDoctorByEmail(user.Email)?.DoctorId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PhotoDataUrl = user.PhotoDataUrl,
                Role = user.Role
            };
        }
    }
}
