using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Migrations;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ClinicDbContext _dbContext;
        public UserRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetByPhoneNumber(string phoneNumber)
        {
            return _dbContext.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public List<User> GetUsersByRole(UserRole role)
        {
            return _dbContext.Users.Where(u => u.Role == role).ToList();
        }
    }
}
