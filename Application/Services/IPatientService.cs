using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPatientService
    {
        int Create(PatientCreateDto dto);
        void Update(PatientUpdateDto dto);
        void Delete(int id);

        List<PatientListItemDto> GetAll();
        PatientDetailsDto? GetById(int id);

        PatientDetailsDto? GetByEmail(string email);
        PatientDetailsDto? GetByPesel(string pesel);
        PatientDetailsDto? GetByPhoneNumber(string phoneNumber);
    }
}
