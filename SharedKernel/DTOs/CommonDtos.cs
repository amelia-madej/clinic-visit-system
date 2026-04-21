using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public List<string> Errors { get; set; } = new();
        public T Data { get; set; } = default!;
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }

    public class MedicationDto
    {
        public int MedicationId { get; set; }
        public string Name { get; set; } = default!;
        public string DosageForm { get; set; } = default!;
        public string Form { get; set; } = default!;
        public decimal StrengthValue { get; set; }
        public string StrengthUnit { get; set; } = default!;
        public string Manufacturer { get; set; } = default!;
        public string? Packaging { get; set; }
        public string ActiveIngredient { get; set; } = default!;
    }
}


