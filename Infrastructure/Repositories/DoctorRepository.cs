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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly ClinicDbContext _dbContext;
        public DoctorRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Doctor> GetAllWithDetails()
        {
            return _dbContext.Doctors.Include(d => d.User).ToList();
        }
        public List<Doctor> GetDoctorsByLastName(string lastName)
        {
            return _dbContext.Doctors.Include(d => d.User).Where(d => d.User.LastName == lastName).ToList();
        }

        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            return _dbContext.Doctors.Include(d => d.User).Where(d => d.Specialization == specialization).ToList();
        }
        public Doctor? GetDoctorByEmail(string email)
        {
            return _dbContext.Doctors.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.User.Email == email);
        }

        public Doctor? GetDoctorByPhoneNumber(string phoneNumber)
        {
            return _dbContext.Doctors.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.User.PhoneNumber == phoneNumber);
        }
        public Doctor? GetByIdWithDetails(int id)
        {
            return _dbContext.Doctors.Include(p => p.User).Include(p => p.Visits).FirstOrDefault(p => p.User.UserId == id);
        }
    }
}
