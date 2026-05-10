using System;
using System.Collections.Generic;
using Domain.Models;
using Infrastructure.Persistence;
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
}
