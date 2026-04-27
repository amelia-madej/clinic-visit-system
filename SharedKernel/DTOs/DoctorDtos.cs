using System;
using System.Collections.Generic;

namespace SharedKernel.DTOs
{
    public class DoctorListItemDto
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Specialization { get; set; } = default!;
    }

    public class DoctorDetailsDto
    {
        public int DoctorId { get; set; }
        public UserDto User { get; set; } = default!;
        public string Specialization { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public List<VisitListItemDto> Visits { get; set; } = new();
    }

    public class DoctorCreateDto
    {
        // User fields
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;

        // Doctor specific
        public string Specialization { get; set; } = default!;
        public string LicenseNumber { get; set; } = default!;
        public string Gender { get; set; } = default!;
    }

    public class DoctorUpdateDto : DoctorCreateDto
    {
        public int DoctorId { get; set; }
    }
}
