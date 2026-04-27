using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IMedicationService
    {
        List<MedicationDto> GetAll();
        MedicationDto? GetById(int id);

        List<MedicationDto> GetByName(string name);
        List<MedicationDto> GetByForm(string form);
        List<MedicationDto> GetByStrength(decimal strengthValue);
        List<MedicationDto> GetByActiveIngredients(List<string> activeIngredients);

        List<MedicationDto> GetByDoctorId(int doctorId);
        List<MedicationDto> GetByPatientId(int patientId);
        List<MedicationDto> GetByPrescriptionId(int prescriptionId);
        List<MedicationDto> GetByVisitId(int visitId);
    }
}
