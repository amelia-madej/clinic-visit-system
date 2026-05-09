using Domain.Contracts;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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
        public List<Patient> GetAllWithDetails()
        {
            return _dbContext.Patients.Include(p => p.User).Include(p => p.Visits).ToList();
        }
        public Patient? GetByIdWithDetails(int id)
        {
            return _dbContext.Patients.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.PatientId == id);
        }
        public Patient? GetPatientByEmail(string email)
        {
            return _dbContext.Patients.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.User.Email == email);
        }

        public Patient? GetPatientByPesel(string pesel)
        {
            return _dbContext.Patients.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.Pesel == pesel);
        }

        public Patient? GetPatientByPhoneNumber(string phoneNumber)
        {
            return _dbContext.Patients.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.User.PhoneNumber == phoneNumber);
        }
    }
}
