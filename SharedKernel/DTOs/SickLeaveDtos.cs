using System;

namespace SharedKernel.DTOs
{
    public class SickLeaveListItemDto
    {
        public int SickLeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }

    public class SickLeaveDetailsDto : SickLeaveListItemDto
    {
        public int MedicalRecordId { get; set; }
    }

    public class SickLeaveCreateDto
    {
        public int MedicalRecordId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } = default!;
    }

    public class SickLeaveUpdateDto : SickLeaveCreateDto
    {
        public int SickLeaveId { get; set; }
    }
}
