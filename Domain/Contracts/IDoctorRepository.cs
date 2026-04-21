using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        List<Doctor> GetDoctorsBySpecialization(string specialization);
        List<Doctor> GetDoctorsByLastName(string lastName);
    }
}
