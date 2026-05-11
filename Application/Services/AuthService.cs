using AutoMapper;
using Domain.Contracts;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClinicUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AuthService(IClinicUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public AuthResponseDto Login(LoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var user = _uow.UserRepository.GetUserByEmail(dto.Email);

            if (user == null)
                throw new Exception("Invalid email or password");

            // NA RAZIE plain text
            if (user.Password != dto.Password)
                throw new Exception("Invalid email or password");

            return new AuthResponseDto
            {
                UserId = user.UserId,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
