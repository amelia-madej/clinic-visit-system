using System;
using FluentValidation;
using SharedKernel.DTOs;

namespace Application.Validators
{
    public class VisitCreateDtoValidator : AbstractValidator<VisitCreateDto>
    {
        public VisitCreateDtoValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0);
            RuleFor(x => x.DoctorId).GreaterThan(0);
            RuleFor(x => x.VisitDateTime).GreaterThan(DateTime.UtcNow.AddMinutes(-5)).WithMessage("VisitDateTime must not be in the past");
            RuleFor(x => x.VisitType).NotEmpty();
        }
    }

    public class VisitUpdateDtoValidator : AbstractValidator<VisitUpdateDto>
    {
        public VisitUpdateDtoValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0);
            RuleFor(x => x.DoctorId).GreaterThan(0);
            RuleFor(x => x.VisitType).NotEmpty();
            RuleFor(x => x.VisitId).GreaterThan(0);
        }
    }

    public class VisitCompleteDtoValidator : AbstractValidator<VisitCompleteDto>
    {
        public VisitCompleteDtoValidator()
        {
            RuleFor(x => x.Diagnosis).NotEmpty();
            RuleFor(x => x.Interview).NotEmpty();
            RuleFor(x => x.SickLeave).SetValidator(new SickLeaveCompleteDtoValidator()!).When(x => x.SickLeave != null);
            RuleForEach(x => x.Prescriptions).SetValidator(new PrescriptionCreateDtoValidator());
        }
    }
}
