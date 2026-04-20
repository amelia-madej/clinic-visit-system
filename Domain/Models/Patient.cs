using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Patient
{
    [Key]
    public int PatientId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }

    public string? PESEL { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    // Navigation
    public ICollection<Visit>? Visits { get; set; }
}
