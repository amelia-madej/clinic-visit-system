using System;
using System.Collections.Generic;

namespace SharedKernel.DTOs
{
    public class PrescriptionListItemDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ValidUntil { get; set; }
    }

    public class PrescriptionDetailsDto : PrescriptionListItemDto
    {
        public List<PrescriptionItemDto> Items { get; set; } = new();
    }

    public class PrescriptionItemDto
    {
        public int Id { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; } = default!;
        public string Dosage { get; set; } = default!;
        public int Quantity { get; set; }
        public string Instructions { get; set; } = default!;
    }

    public class PrescriptionCreateDto
    {
        public DateTime? ValidUntil { get; set; }
        public List<PrescriptionItemCreateDto> Items { get; set; } = new();
    }

    public class PrescriptionItemCreateDto
    {
        public int MedicationId { get; set; }
        public string Dosage { get; set; } = default!;
        public int Quantity { get; set; }
        public string Instructions { get; set; } = default!;
    }
}
