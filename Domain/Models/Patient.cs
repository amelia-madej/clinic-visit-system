using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Domain.Models;

public class Patient
{
    public int PatientId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public string Pesel { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = null!;
    public List<Visit>? Visits { get; set; }
}
