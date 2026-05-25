using FluentValidation;
using SharedKernel.DTOs;

namespace Application.Validators
{
    public class PatientCreateDtoValidator : AbstractValidator<PatientCreateDto>
    {
        public PatientCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{6,20}$")
                .WithMessage("Phone number must contain 6 to 20 digits.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Pesel)
                .NotEmpty()
                .Matches(@"^\d{11}$")
                .WithMessage("PESEL must contain exactly 11 digits.");
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow).WithMessage("DateOfBirth must be in the past");
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Gender).IsInEnum();
        }
    }

    public class PatientUpdateDtoValidator : AbstractValidator<PatientUpdateDto>
    {
        public PatientUpdateDtoValidator()
        {
            Include(new PatientCreateDtoValidator());
            RuleFor(x => x.PatientId).GreaterThan(0);
        }
    }
}
