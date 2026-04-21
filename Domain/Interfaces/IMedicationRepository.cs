using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        List<Medication> GetMedicationsByPatientId(int patientId);
        List<Medication> GetMedicationsByDoctorId(int doctorId);
        List<Medication> GetMedicationsByVisitId(int visitId);
        List<Medication> GetMedicationsByPrescriptionId(int prescriptionId);
        List<Medication> GetMedicationsByName(string name);
        List<Medication> GetMedicationsByActiveIngredient(List<string> activeIngredients);
        List<Medication> GetMedicationsByForm(string form);
        List<Medication> GetMedicationsByStrengthValue(decimal strengthValue);
    }
}
