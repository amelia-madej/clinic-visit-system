using Domain.Models;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetUsersByRole(UserRole role);
        User GetByEmail(string email);
        User GetByPhoneNumber(string phoneNumber);
    }
}
