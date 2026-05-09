using SharedKernel.DTOs;
using System;
using System.Collections.Generic;

namespace SharedKernel.DTOs
{
    public class VisitListItemDto
    {
        public int VisitId { get; set; }
        public DateTime VisitDateTime { get; set; }
        public string Status { get; set; } = default!;
        public string VisitType { get; set; } = default!;
        public PatientListItemDto? Patient { get; set; }
        public DoctorListItemDto? Doctor { get; set; }
    }

    public class VisitDetailsDto
    {
        public int VisitId { get; set; }
        public DateTime VisitDateTime { get; set; }
        public string Status { get; set; } = default!;
        public string VisitType { get; set; } = default!;
        public PatientDetailsDto? Patient { get; set; }
        public DoctorDetailsDto? Doctor { get; set; }
        public MedicalRecordDto? MedicalRecord { get; set; }
    }

    public class VisitCreateDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime VisitDateTime { get; set; }
        public string VisitType { get; set; } = default!;
    }

    public class VisitUpdateDto : VisitCreateDto
    {
        public int VisitId { get; set; }
        public string? Status { get; set; }
    }

    public class VisitCompleteDto
    {
        public string Interview { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public string Recommendations { get; set; } = default!;
        public SickLeaveCompleteDto? SickLeave { get; set; }
        public List<PrescriptionCreateDto> Prescriptions { get; set; } = new();
    }
}
