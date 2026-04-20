using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public enum UserRole
{
    Admin,
    Doctor,
    Patient
}

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    // Store hashed password in production
    public string? Password { get; set; }

    public UserRole Role { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}
