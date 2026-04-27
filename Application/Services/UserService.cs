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
    public class UserService : IUserService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IValidator<UpdateUserDto> _updateValidator;

        public UserService(
            IClinicUnitOfWork clinicUnitOfWork,
            IMapper mapper,
            IValidator<CreateUserDto> createValidator,
            IValidator<UpdateUserDto> updateValidator)
        {
            _uow = clinicUnitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public int Create(CreateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _createValidator.ValidateAndThrow(dto);

            var existingUser = _uow.UserRepository.GetUserByEmail(dto.Email);
            if (existingUser != null)
                throw new Exception("User with this email already exists");

            var user = _mapper.Map<User>(dto);

            _uow.UserRepository.Insert(user);
            _uow.Commit();

            return user.UserId;
        }

        public void Delete(int id)
        {
            if(id<= 0)
                throw new ArgumentException("Invalid user ID", nameof(id));

            var user = _uow.UserRepository.Get(id);
            if (user == null)
                throw new Exception("User not found");

            _uow.UserRepository.Delete(user);
            _uow.Commit();
        }

        public List<UserDto> GetAll()
        {
            var users = _uow.UserRepository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }

        public UserDto? GetByEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            var user = _uow.UserRepository.GetUserByEmail(email);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public UserDto? GetById(int id)
        {
            if(id<= 0) 
                throw new ArgumentException("Invalid user ID", nameof(id));

            var user = _uow.UserRepository.Get(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public UserDto? GetByPhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

            var user = _uow.UserRepository.GetUserByPhoneNumber(phoneNumber);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetByRole(UserRole role)
        {
            if(!Enum.IsDefined(typeof(UserRole), role))
                throw new ArgumentException("Invalid user role", nameof(role));

            var users = _uow.UserRepository.GetUsersByRole(role);
            return _mapper.Map<List<UserDto>>(users);
        }

        public void Update(UpdateUserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            _updateValidator.ValidateAndThrow(dto);

            var user = _uow.UserRepository.Get(dto.UserId);
            if (user == null)
                throw new Exception("User not found");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Role = dto.Role;

            _uow.Commit();
        }
    }
}
