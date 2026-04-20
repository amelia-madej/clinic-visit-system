using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Specialization { get; set; }
    public string Gender { get; set; }
    public string LicenseNumber { get; set; }
    public ICollection<Visit>? Visits { get; set; }
}
