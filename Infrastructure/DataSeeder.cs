using System;
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

                var patients = new List<Patient>
                {
                    new Patient { UserId = users[1].UserId, Pesel = "12345678901", Gender = Gender.Female, DateOfBirth = new DateTime(1990, 1, 1), Address = "123 Main St" }
                };
                _dbContext.Patients.AddRange(patients);
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
