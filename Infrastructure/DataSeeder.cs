using System;
using System.Collections.Generic;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure;

public class DataSeeder
{
    private readonly ClinicDbContext _dbContext;
    public DataSeeder(ClinicDbContext context)
    {
        this._dbContext = context;
    }

    public void Seed()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        if (_dbContext.Database.CanConnect())
        {
           if (!_dbContext.Users.Any())
            {
                // Seed sample data for testing
                var users = new List<User>
                {
                    new User { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789", Password = "password123", Role = UserRole.Doctor },
                    new User { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321", Password = "password123", Role = UserRole.Patient },
                    new User { FirstName = "Admin", LastName = "User", Email = "admin@example.com", PhoneNumber = "555555555", Password = "admin123", Role = UserRole.Admin }
                };
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();

                var doctors = new List<Doctor>
                {
                    new Doctor { UserId = users[0].UserId, Specialization = "Cardiology", Gender = Gender.Male, LicenseNumber = "LIC123" }
                };
                _dbContext.Doctors.AddRange(doctors);
                _dbContext.SaveChanges();

                var patientUsers = new List<User>
                {
                    users[1],
                    new User { FirstName = "Anna",   LastName = "Kowalska",  Email = "anna.kowalska@example.com",  PhoneNumber = "111222333", Password = "password123", Role = UserRole.Patient },
                    new User { FirstName = "Marek",  LastName = "Nowak",     Email = "marek.nowak@example.com",    PhoneNumber = "444555666", Password = "password123", Role = UserRole.Patient },
                };
                _dbContext.Users.AddRange(patientUsers[1], patientUsers[2]);
                _dbContext.SaveChanges();

                var patients = new List<Patient>
                {
                    new Patient { UserId = patientUsers[0].UserId, Pesel = "12345678901", Gender = Gender.Female, DateOfBirth = new DateTime(1990, 1, 1),  Address = "123 Main St" },
                    new Patient { UserId = patientUsers[1].UserId, Pesel = "98765432100", Gender = Gender.Female, DateOfBirth = new DateTime(1985, 6, 15), Address = "ul. Kwiatowa 5, Warszawa" },
                    new Patient { UserId = patientUsers[2].UserId, Pesel = "55031208193", Gender = Gender.Male,   DateOfBirth = new DateTime(1955, 3, 12), Address = "ul. Lipowa 12, Kraków" },
                };
                _dbContext.Patients.AddRange(patients);
                _dbContext.SaveChanges();

                var visits = new List<Visit>
                {
                    new Visit { PatientId = patients[0].PatientId, DoctorId = doctors[0].DoctorId, VisitDateTime = DateTime.Today.AddDays(3).AddHours(9),   Status = VisitStatus.Scheduled,  VisitType = VisitType.InPerson,     CreatedAt = DateTime.Now },
                    new Visit { PatientId = patients[0].PatientId, DoctorId = doctors[0].DoctorId, VisitDateTime = DateTime.Today.AddDays(-10).AddHours(11), Status = VisitStatus.Completed,  VisitType = VisitType.InPerson,     CreatedAt = DateTime.Now },
                    new Visit { PatientId = patients[1].PatientId, DoctorId = doctors[0].DoctorId, VisitDateTime = DateTime.Today.AddDays(7).AddHours(14),   Status = VisitStatus.Scheduled,  VisitType = VisitType.Telemedicine, CreatedAt = DateTime.Now },
                    new Visit { PatientId = patients[1].PatientId, DoctorId = doctors[0].DoctorId, VisitDateTime = DateTime.Today.AddDays(-3).AddHours(10),  Status = VisitStatus.Cancelled,  VisitType = VisitType.InPerson,     CreatedAt = DateTime.Now },
                    new Visit { PatientId = patients[2].PatientId, DoctorId = doctors[0].DoctorId, VisitDateTime = DateTime.Today.AddDays(1).AddHours(8),    Status = VisitStatus.Scheduled,  VisitType = VisitType.HomeVisit,    CreatedAt = DateTime.Now },
                    new Visit { PatientId = patients[2].PatientId, DoctorId = doctors[0].DoctorId, VisitDateTime = DateTime.Today.AddDays(-21).AddHours(16), Status = VisitStatus.Completed,  VisitType = VisitType.InPerson,     CreatedAt = DateTime.Now },
                };
                _dbContext.Visits.AddRange(visits);
                _dbContext.SaveChanges();

                var medicalRecords = new List<MedicalRecord>
                {
                    new MedicalRecord
                    {
                        VisitId         = visits[1].VisitId,
                        Interview       = "Patient reports recurring chest tightness on exertion, lasting a few minutes, relieved by rest. No radiation to the arm or jaw. No shortness of breath at rest.",
                        Diagnosis       = "Stable angina pectoris",
                        Recommendations = "Continue current beta-blocker therapy. Avoid strenuous exercise. Follow up in 4 weeks or sooner if symptoms worsen. ECG scheduled.",
                        CreatedAt       = DateTime.UtcNow
                    },
                    new MedicalRecord
                    {
                        VisitId         = visits[5].VisitId,
                        Interview       = "Patient presents with lower back pain for the past 2 weeks following heavy lifting. Pain rated 6/10, worsened by bending forward. No leg radiation, no bladder/bowel issues.",
                        Diagnosis       = "Acute lumbar strain",
                        Recommendations = "Rest for 3 days, avoid heavy lifting for 4 weeks. Ibuprofen 400mg as needed with food. Gentle stretching after acute phase. Physiotherapy referral if no improvement in 2 weeks.",
                        CreatedAt       = DateTime.UtcNow,
                        SickLeave       = new SickLeave
                        {
                            StartDate = DateTime.Today.AddDays(-21),
                            EndDate   = DateTime.Today.AddDays(-14),
                            Reason    = "Acute lumbar strain — unable to perform physical work",
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                };
                _dbContext.MedicalRecords.AddRange(medicalRecords);
                _dbContext.SaveChanges();

                // Temporary seed data - will be replaced by CSV import
                var medications = new List<Medication>
                {
                    new Medication { Name = "Ibuprofen", DosageForm = "orally", Form = "tablet", StrengthValue = 400, StrengthUnit = "mg", Manufacturer = "Pfizer", Packaging = "box of 20 tablets", ActiveIngredient = "Ibuprofen" },
                    new Medication { Name = "Amoxicillin", DosageForm = "orally", Form = "capsule", StrengthValue = 500, StrengthUnit = "mg", Manufacturer = "GSK", Packaging = "box of 21 capsules", ActiveIngredient = "Amoxicillin" },
                    new Medication { Name = "Paracetamol", DosageForm = "orally", Form = "tablet", StrengthValue = 500, StrengthUnit = "mg", Manufacturer = "Bayer", Packaging = "box of 24 tablets", ActiveIngredient = "Paracetamol" }
                };
                _dbContext.Medications.AddRange(medications);
                _dbContext.SaveChanges();
            }
        }
    }

    public void SeedRandomData()
    {
        _dbContext.Database.EnsureCreated();

        if (_dbContext.Users.Any(u => u.Email == "admin@clinic.local") && _dbContext.Visits.Count() >= 100)
            return;

        var random = new Random(42);
        var firstNames = new[] { "Adam", "Ewa", "Piotr", "Maria", "Tomasz", "Katarzyna", "Michal", "Anna", "Pawel", "Julia", "Marek", "Alicja" };
        var lastNames = new[] { "Nowak", "Kowalski", "Wisniewski", "Zielinski", "Wojcik", "Kaminska", "Lewandowski", "Dabrowska", "Mazur", "Krawczyk" };
        var specializations = new[] { "Cardiology", "Family Medicine", "Orthopedics", "Neurology", "Dermatology", "Pulmonology" };
        var diagnoses = new[] { "Upper respiratory infection", "Lumbar strain", "Hypertension follow-up", "Migraine", "Bronchitis", "Dermatitis", "Joint pain" };

        if (!_dbContext.Users.Any(u => u.Email == "admin@clinic.local"))
        {
            var admin = new User
            {
                FirstName = "Admin",
                LastName = "Clinic",
                Email = "admin@clinic.local",
                PhoneNumber = "700000000",
                Password = "admin123",
                Role = UserRole.Admin
            };

            _dbContext.Users.Add(admin);
            _dbContext.SaveChanges();
        }

        var doctors = new List<Doctor>();
        for (var i = 0; i < 6; i++)
        {
            var email = $"doctor{i + 1}@clinic.local";
            if (_dbContext.Users.Any(u => u.Email == email))
                continue;

            var user = new User
            {
                FirstName = firstNames[i],
                LastName = lastNames[i],
                Email = email,
                PhoneNumber = (600000000 + i).ToString(),
                Password = "password123",
                Role = UserRole.Doctor
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            doctors.Add(new Doctor
            {
                UserId = user.UserId,
                Specialization = specializations[i],
                Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
                LicenseNumber = $"LIC-DEMO-{1000 + i}"
            });
        }

        _dbContext.Doctors.AddRange(doctors);
        _dbContext.SaveChanges();

        var patients = new List<Patient>();
        for (var i = 0; i < 40; i++)
        {
            var email = $"patient{i + 1}@clinic.local";
            if (_dbContext.Users.Any(u => u.Email == email))
                continue;

            var user = new User
            {
                FirstName = firstNames[random.Next(firstNames.Length)],
                LastName = lastNames[random.Next(lastNames.Length)],
                Email = email,
                PhoneNumber = (500000000 + i).ToString(),
                Password = "password123",
                Role = UserRole.Patient
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            patients.Add(new Patient
            {
                UserId = user.UserId,
                Pesel = (80010100000L + i).ToString(),
                Gender = i % 2 == 0 ? Gender.Female : Gender.Male,
                DateOfBirth = DateTime.Today.AddYears(-random.Next(18, 82)).AddDays(random.Next(365)),
                Address = $"ul. Testowa {i + 1}, Warszawa"
            });
        }

        _dbContext.Patients.AddRange(patients);
        _dbContext.SaveChanges();

        var savedDoctors = _dbContext.Doctors.Include(d => d.User).ToList();
        var savedPatients = _dbContext.Patients.Include(p => p.User).ToList();

        if (savedDoctors.Count == 0 || savedPatients.Count == 0 || _dbContext.Visits.Count() >= 100)
            return;

        var anomalyDoctor = savedDoctors.First();
        var visits = new List<Visit>();

        for (var i = 0; i < 160; i++)
        {
            var visitDate = DateTime.Today.AddDays(random.Next(-75, 25)).AddHours(random.Next(8, 17));
            var status = visitDate > DateTime.Now
                ? VisitStatus.Scheduled
                : random.NextDouble() < 0.82 ? VisitStatus.Completed : VisitStatus.Cancelled;

            visits.Add(new Visit
            {
                PatientId = savedPatients[random.Next(savedPatients.Count)].PatientId,
                DoctorId = i < 35 ? anomalyDoctor.DoctorId : savedDoctors[random.Next(savedDoctors.Count)].DoctorId,
                VisitDateTime = visitDate,
                Status = status,
                VisitType = (VisitType)random.Next(0, 3),
                CreatedAt = visitDate.AddDays(-random.Next(1, 14))
            });
        }

        _dbContext.Visits.AddRange(visits);
        _dbContext.SaveChanges();

        var completedVisits = _dbContext.Visits
            .Where(v => v.Status == VisitStatus.Completed)
            .OrderBy(v => v.VisitDateTime)
            .ToList();

        var medicalRecords = new List<MedicalRecord>();
        foreach (var visit in completedVisits)
        {
            var shouldCreateSickLeave = visit.DoctorId == anomalyDoctor.DoctorId
                ? random.NextDouble() < 0.85
                : random.NextDouble() < 0.18;

            var record = new MedicalRecord
            {
                VisitId = visit.VisitId,
                Interview = "Demo interview generated for testing.",
                Diagnosis = diagnoses[random.Next(diagnoses.Length)],
                Recommendations = "Follow medical recommendations and schedule control visit if symptoms persist.",
                CreatedAt = visit.VisitDateTime.AddHours(1)
            };

            if (shouldCreateSickLeave)
            {
                var duration = visit.DoctorId == anomalyDoctor.DoctorId
                    ? random.Next(5, 21)
                    : random.Next(2, 8);

                record.SickLeave = new SickLeave
                {
                    StartDate = visit.VisitDateTime.Date,
                    EndDate = visit.VisitDateTime.Date.AddDays(duration - 1),
                    Reason = record.Diagnosis,
                    CreatedAt = visit.VisitDateTime.AddHours(1)
                };
            }

            medicalRecords.Add(record);
        }

        _dbContext.MedicalRecords.AddRange(medicalRecords);
        _dbContext.SaveChanges();
    }

    public int EnsureBaselineAnomalyData()
    {
        _dbContext.Database.EnsureCreated();
        RemoveLegacyAnomalyTestData();
        RemoveBaselineAnomalyData();

        var marker = "[ANOMALY-BASELINE]";

        var random = new Random(2026);
        var doctors = _dbContext.Doctors.Include(d => d.User).ToList();
        if (doctors.Count == 0)
            return 0;

        var patients = _dbContext.Patients.Include(p => p.User).Take(12).ToList();
        if (patients.Count == 0)
            return 0;

        var meds = _dbContext.Medications.ToList();
        if (meds.Count < 4)
            return 0;

        var opioid = meds.FirstOrDefault(m => m.ActiveIngredient.Contains("tramadol", StringComparison.OrdinalIgnoreCase))
                     ?? meds.First();
        var benzo = meds.FirstOrDefault(m => m.ActiveIngredient.Contains("diazepam", StringComparison.OrdinalIgnoreCase)
                                          || m.ActiveIngredient.Contains("alprazolam", StringComparison.OrdinalIgnoreCase))
                    ?? meds.Skip(1).First();
        var cardio = meds.FirstOrDefault(m => m.ActiveIngredient.Contains("atorvastatin", StringComparison.OrdinalIgnoreCase))
                     ?? meds.Skip(2).First();
        var outside = meds.FirstOrDefault(m => m.ActiveIngredient.Contains("clotrimazole", StringComparison.OrdinalIgnoreCase))
                      ?? meds.Skip(3).First();

        var created = 0;
        var baseDate = DateTime.Today.AddDays(-75);
        var visitTypes = new[] { VisitType.InPerson, VisitType.Telemedicine, VisitType.HomeVisit };

        for (var d = 0; d < doctors.Count; d++)
        {
            var doctor = doctors[d];
            var heavyDoctor = d < 3;

            for (var i = 0; i < 18; i++)
            {
                var day = heavyDoctor ? (i % 2 == 0 ? 1 + (i % 3) : 28 + (i % 3)) : 8 + (i % 14);
                var visitDate = new DateTime(baseDate.Year, baseDate.Month, 1).AddMonths(i / 6).AddDays(day - 1).AddHours(8 + (i % 8));
                if (visitDate > DateTime.Today.AddHours(-2))
                    visitDate = DateTime.Today.AddDays(-2 - (i % 10)).AddHours(9 + (i % 6));

                var patient = patients[(d * 3 + i) % patients.Count];
                var visit = new Visit
                {
                    PatientId = patient.PatientId,
                    DoctorId = doctor.DoctorId,
                    VisitDateTime = visitDate,
                    Status = VisitStatus.Completed,
                    VisitType = visitTypes[(d + i) % visitTypes.Length],
                    CreatedAt = visitDate.AddHours(-2)
                };
                _dbContext.Visits.Add(visit);
                _dbContext.SaveChanges();

                var weakDiagnosis = heavyDoctor && i % 3 != 0 ? "control" : "respiratory infection";
                var record = new MedicalRecord
                {
                    VisitId = visit.VisitId,
                    Interview = $"{marker} Follow-up and symptoms check.",
                    Diagnosis = weakDiagnosis,
                    Recommendations = "Rest and monitoring.",
                    CreatedAt = visitDate.AddMinutes(20)
                };
                _dbContext.MedicalRecords.Add(record);
                _dbContext.SaveChanges();

                var shouldCreateLeave = heavyDoctor ? i < 14 : i % 7 == 0;
                if (shouldCreateLeave)
                {
                    _dbContext.SickLeaves.Add(new SickLeave
                    {
                        MedicalRecordId = record.MedicalRecordId,
                        StartDate = visitDate.Date,
                        EndDate = heavyDoctor ? visitDate.Date.AddDays(11 + (i % 6)) : visitDate.Date.AddDays(2 + (i % 2)),
                        Reason = "Temporary inability to work",
                        CreatedAt = visitDate.AddMinutes(30)
                    });
                }

                var prescription = new Prescription
                {
                    MedicalRecordId = record.MedicalRecordId,
                    CreatedAt = visitDate.AddMinutes(35),
                    ValidUntil = visitDate.AddDays(30)
                };
                _dbContext.Prescriptions.Add(prescription);
                _dbContext.SaveChanges();

                var medsForVisit = new List<Medication>();
                if (heavyDoctor && i < 12)
                {
                    medsForVisit.Add(opioid);
                    medsForVisit.Add(benzo);
                    if (i < 8) medsForVisit.Add(opioid);
                }
                else
                {
                    medsForVisit.Add(cardio);
                    if (i % 3 == 0) medsForVisit.Add(outside);
                }

                foreach (var med in medsForVisit)
                {
                    _dbContext.PrescriptionItems.Add(new PrescriptionItem
                    {
                        PrescriptionId = prescription.PrescriptionId,
                        MedicationId = med.MedicationId,
                        Dosage = "1-0-1",
                        Quantity = 2 + random.Next(4),
                        Instructions = "After meal"
                    });
                }

                created++;
            }
        }

        _dbContext.SaveChanges();
        return created;
    }

    private void RemoveLegacyAnomalyTestData()
    {
        var marker = "[ANOMALY-TEST]";
        var records = _dbContext.MedicalRecords
            .Where(m => m.Interview.Contains(marker))
            .Select(m => m.MedicalRecordId)
            .ToList();

        if (records.Count == 0)
            return;

        var visits = _dbContext.MedicalRecords
            .Where(m => records.Contains(m.MedicalRecordId))
            .Select(m => m.VisitId)
            .ToList();

        var prescriptionIds = _dbContext.Prescriptions
            .Where(p => records.Contains(p.MedicalRecordId))
            .Select(p => p.PrescriptionId)
            .ToList();

        var items = _dbContext.PrescriptionItems.Where(i => prescriptionIds.Contains(i.PrescriptionId));
        var prescriptions = _dbContext.Prescriptions.Where(p => prescriptionIds.Contains(p.PrescriptionId));
        var sickLeaves = _dbContext.SickLeaves.Where(s => records.Contains(s.MedicalRecordId));
        var medicalRecords = _dbContext.MedicalRecords.Where(m => records.Contains(m.MedicalRecordId));
        var visitsToDelete = _dbContext.Visits.Where(v => visits.Contains(v.VisitId));

        _dbContext.PrescriptionItems.RemoveRange(items);
        _dbContext.Prescriptions.RemoveRange(prescriptions);
        _dbContext.SickLeaves.RemoveRange(sickLeaves);
        _dbContext.MedicalRecords.RemoveRange(medicalRecords);
        _dbContext.Visits.RemoveRange(visitsToDelete);
        _dbContext.SaveChanges();
    }

    private void RemoveBaselineAnomalyData()
    {
        var marker = "[ANOMALY-BASELINE]";
        var records = _dbContext.MedicalRecords
            .Where(m => m.Interview.Contains(marker))
            .Select(m => m.MedicalRecordId)
            .ToList();

        if (records.Count == 0)
            return;

        var visits = _dbContext.MedicalRecords
            .Where(m => records.Contains(m.MedicalRecordId))
            .Select(m => m.VisitId)
            .ToList();

        var prescriptionIds = _dbContext.Prescriptions
            .Where(p => records.Contains(p.MedicalRecordId))
            .Select(p => p.PrescriptionId)
            .ToList();

        _dbContext.PrescriptionItems.RemoveRange(_dbContext.PrescriptionItems.Where(i => prescriptionIds.Contains(i.PrescriptionId)));
        _dbContext.Prescriptions.RemoveRange(_dbContext.Prescriptions.Where(p => prescriptionIds.Contains(p.PrescriptionId)));
        _dbContext.SickLeaves.RemoveRange(_dbContext.SickLeaves.Where(s => records.Contains(s.MedicalRecordId)));
        _dbContext.MedicalRecords.RemoveRange(_dbContext.MedicalRecords.Where(m => records.Contains(m.MedicalRecordId)));
        _dbContext.Visits.RemoveRange(_dbContext.Visits.Where(v => visits.Contains(v.VisitId)));
        _dbContext.SaveChanges();
    }
}
