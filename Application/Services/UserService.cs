using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IClinicUnitOfWork _clinicUnitOfWork;
        private readonly IMapper _mapper;
        public UserService(IClinicUnitOfWork clinicUnitOfWork, IMapper mapper)
        {
            _clinicUnitOfWork = clinicUnitOfWork;
            _mapper = mapper;
        }
        public void Create(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public List<User> GetByRole(UserRole role)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
