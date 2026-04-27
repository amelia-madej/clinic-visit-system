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
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(6).MaximumLength(20);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.PESEL).NotEmpty().Length(11);
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow).WithMessage("DateOfBirth must be in the past");
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
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
