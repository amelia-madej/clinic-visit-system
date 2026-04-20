using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public enum VisitStatus
{
    Scheduled,
    Completed,
    Cancelled
}

public enum VisitType
{
    InPerson,
    Telemedicine,
    HomeVisit
}

public class Visit
{
    [Key]
    public int VisitId { get; set; }

    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public DateTime VisitDateTime { get; set; }

    public VisitStatus Status { get; set; }

    public VisitType VisitType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
