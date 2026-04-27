using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient? GetPatientByEmail(string email);
        Patient? GetPatientByPesel(string pesel);
        Patient? GetPatientByPhoneNumber(string phoneNumber);
        Patient? GetByIdWithDetails(int id);
    }
}
