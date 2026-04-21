using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Patient? GetByEmail(string email);
        Patient? GetByPesel(string pesel);
        Patient? GetByPhoneNumber(string phoneNumber);

    }
}
