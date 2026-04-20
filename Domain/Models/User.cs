using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharedKernel;

namespace Domain.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}
