using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        private readonly ClinicDbContext _dbContext;
        public MedicationRepository(ClinicDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Medication> GetMedicationsByActiveIngredient(List<string> activeIngredients)
        {
            return _dbContext.Medications
                .Where(m => activeIngredients.Contains(m.ActiveIngredient))
                .ToList();
        }

        public List<Medication> GetMedicationsByDoctorId(int doctorId)
        {
            if (_dbContext.PrescriptionItems == null)
            {
                return new List<Medication>();
            }
            return _dbContext.Medications.Where(m => m.PrescriptionItems.Any(p => p.Prescription.MedicalRecord.Visit.DoctorId == doctorId)).ToList();
        }

        public List<Medication> GetMedicationsByForm(string form)
        {
            return _dbContext.Medications.Where(m => m.Form == form).ToList();
        }

        public List<Medication> GetMedicationsByName(string name)
        {
            return _dbContext.Medications.Where(m => m.Name == name).ToList();
        }

        public List<Medication> GetMedicationsByPatientId(int patientId)
        {
            if (_dbContext.PrescriptionItems == null)
            {
                return new List<Medication>();
            }
            return _dbContext.Medications
                .Where(m => m.PrescriptionItems.Any(p => p.Prescription.MedicalRecord.Visit.PatientId == patientId))
                .ToList();
        }

        public List<Medication> GetMedicationsByPrescriptionId(int prescriptionId)
        {
            if(_dbContext.PrescriptionItems == null || _dbContext.Medications == null)
            {
                return new List<Medication>();
            }
            return _dbContext.Medications
                .Where(m => m.PrescriptionItems.Any(p => p.Prescription.PrescriptionId == prescriptionId))
                .ToList();
        }

        public List<Medication> GetMedicationsByStrengthValue(decimal strengthValue)
        {
            return _dbContext.Medications.Where(m => m.StrengthValue == strengthValue).ToList();
        }

        public List<Medication> GetMedicationsByVisitId(int visitId)
        {
            if (_dbContext.PrescriptionItems == null)
            {
                return new List<Medication>();
            }
            return _dbContext.Medications.Where(m => m.PrescriptionItems.Any(p => p.Prescription.MedicalRecord.VisitId == visitId)).ToList();
        }
    }
}
