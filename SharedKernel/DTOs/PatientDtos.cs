using System;
using System.Collections.Generic;

namespace SharedKernel.DTOs
{
    public class PatientListItemDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public DateTime? LastVisitDate { get; set; }
    }

    public class PatientDetailsDto
    {
        public int PatientId { get; set; }
        public UserDto User { get; set; } = default!;
        public string PESEL { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = default!;
        public List<VisitListItemDto> Visits { get; set; } = new();
    }

    public class PatientCreateDto
    {
        // User fields
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;

        // Patient specific
        public string Pesel { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
    }

    public class PatientUpdateDto : PatientCreateDto
    {
        public int PatientId { get; set; }
    }
}
