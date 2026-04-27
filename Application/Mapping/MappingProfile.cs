using AutoMapper;
using Domain.Models;
using Domain;
using SharedKernel.DTOs;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Patient
            CreateMap<Patient, PatientListItemDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
                .ForMember(d => d.Age, opt => opt.MapFrom(s =>
                    DateTime.UtcNow.Year - s.DateOfBirth.Year - (DateTime.UtcNow < s.DateOfBirth.AddYears(DateTime.UtcNow.Year - s.DateOfBirth.Year) ? 1 : 0)))
                .ForMember(d => d.LastVisitDate, opt => opt.MapFrom(s =>
                        s.Visits != null && s.Visits.Any()
                        ? s.Visits.Max(v => v.VisitDateTime)
                        : (DateTime?)null));

            CreateMap<Patient, PatientDetailsDto>()
                .ForMember(d => d.User, opt => opt.MapFrom(s => s.User));

            CreateMap<PatientCreateDto, User>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password));


            CreateMap<PatientCreateDto, Patient>()
                .ForMember(d => d.Pesel, opt => opt.MapFrom(s => s.Pesel))
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender));

            // Doctor
            CreateMap<Doctor, DoctorListItemDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Specialization, opt => opt.MapFrom(s => s.Specialization));

            CreateMap<DoctorCreateDto, User>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password));

            CreateMap<DoctorCreateDto, Doctor>()
                .ForMember(d => d.Specialization, opt => opt.MapFrom(s => s.Specialization))
                .ForMember(d => d.LicenseNumber, opt => opt.MapFrom(s => s.LicenseNumber))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender));

            // Visit
            CreateMap<Visit, VisitListItemDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.VisitType, opt => opt.MapFrom(s => s.VisitType.ToString()));

            CreateMap<Visit, VisitDetailsDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.VisitType, opt => opt.MapFrom(s => s.VisitType.ToString()));

            CreateMap<VisitCreateDto, Visit>();

            // MedicalRecord
            CreateMap<MedicalRecord, MedicalRecordDto>();

            // Prescription
            CreateMap<Prescription, PrescriptionListItemDto>();
            CreateMap<Prescription, PrescriptionDetailsDto>();
            CreateMap<PrescriptionItem, PrescriptionItemDto>()
                .ForMember(d => d.MedicationName, opt => opt.MapFrom(s => s.Medication != null ? s.Medication.Name : string.Empty));

            CreateMap<PrescriptionCreateDto, Prescription>();
            CreateMap<PrescriptionItemCreateDto, PrescriptionItem>();

            // Medication
            CreateMap<Medication, MedicationDto>();
        }
    }
}
