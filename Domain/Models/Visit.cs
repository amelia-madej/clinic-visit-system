using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Domain.Models;

public class Visit
{
    public int VisitId { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }
    public DateTime VisitDateTime { get; set; }
    public VisitStatus Status { get; set; }
    public VisitType VisitType { get; set; }
    public DateTime CreatedAt { get; set; }
}
