using FluentValidation;
using SharedKernel.DTOs;

namespace Application.Validators
{
    public class DoctorCreateDtoValidator : AbstractValidator<DoctorCreateDto>
    {
        public DoctorCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(6).MaximumLength(20);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Specialization).NotEmpty();
            RuleFor(x => x.LicenseNumber).NotEmpty();
            RuleFor(x => x.Gender).IsInEnum();
        }
    }

    public class DoctorUpdateDtoValidator : AbstractValidator<DoctorUpdateDto>
    {
        public DoctorUpdateDtoValidator()
        {
            Include(new DoctorCreateDtoValidator());
            RuleFor(x => x.DoctorId).GreaterThan(0);
        }
    }
}
