using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharedKernel;

namespace Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? PhotoDataUrl { get; set; }
    public UserRole Role { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}
