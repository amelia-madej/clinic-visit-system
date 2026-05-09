using SharedKernel.DTOs;
using System;
using System.Collections.Generic;

namespace SharedKernel.DTOs
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public string Interview { get; set; } = default!;
        public string Diagnosis { get; set; } = default!;
        public string Recommendations { get; set; } = default!;
        public List<PrescriptionListItemDto> Prescriptions { get; set; } = new();
        public SickLeaveListItemDto? SickLeave { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
