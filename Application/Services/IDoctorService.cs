using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IDoctorService
    {
        int Create(DoctorCreateDto dto);
        void Update(DoctorUpdateDto dto);
        void Delete(int id);

        List<DoctorListItemDto> GetAll();
        DoctorDetailsDto? GetById(int id);

        DoctorDetailsDto? GetDoctorsByLastName(string lastName);
        DoctorDetailsDto? GetDoctorsBySpecialization(string specialization);
    }
}
