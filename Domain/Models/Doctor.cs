using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Domain.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public string Specialization { get; set; } = null!;
    public Gender Gender { get; set; }
    public string LicenseNumber { get; set; } = null!;
    public List<Visit>? Visits { get; set; }
}
