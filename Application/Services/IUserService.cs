using Domain.Models;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        User? GetById(int id);
        User? GetByEmail(string email);
        User? GetByPhoneNumber(string phoneNumber);
        List<User> GetAll();
        List<User> GetByRole(UserRole role);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
