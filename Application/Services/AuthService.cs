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
        private readonly IPasswordHashService _passwordHashService;

        public AuthService(
            IClinicUnitOfWork uow,
            IMapper mapper,
            IValidator<LoginDto> loginValidator,
            IPasswordHashService passwordHashService)
        {
            _uow = uow;
            _mapper = mapper;
            _loginValidator = loginValidator;
            _passwordHashService = passwordHashService;
        }

        public AuthResponseDto Login(LoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _loginValidator.ValidateAndThrow(dto);

            var user = _uow.UserRepository.GetUserByEmail(dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password");

            if (!_passwordHashService.Verify(dto.Password, user.Password))
                throw new Exception("Invalid email or password");

            if (!_passwordHashService.IsHash(user.Password))
            {
                user.Password = _passwordHashService.Hash(dto.Password);
                _uow.Commit();
            }

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
