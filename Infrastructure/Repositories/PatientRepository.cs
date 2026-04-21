using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly ClinicDbContext _dbContext;
        public PatientRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Patient? GetByEmail(string email)
        {
            return _dbContext.Patients.FirstOrDefault(p => p.User.Email == email);
        }

        public Patient? GetByPesel(string pesel)
        {
            return _dbContext.Patients.FirstOrDefault(p => p.Pesel == pesel);
        }

        public Patient? GetByPhoneNumber(string phoneNumber)
        {
            return _dbContext.Patients.FirstOrDefault(p => p.User.PhoneNumber == phoneNumber);
        }
    }
}
