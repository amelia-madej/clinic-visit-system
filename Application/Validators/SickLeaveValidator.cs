using FluentValidation;
using SharedKernel.DTOs;
using System;

namespace Application.Validators
{
    public class SickLeaveCreateDtoValidator : AbstractValidator<SickLeaveCreateDto>
    {
        public SickLeaveCreateDtoValidator()
        {
            RuleFor(x => x.MedicalRecordId).GreaterThan(0).WithMessage("Medical record ID must be greater than 0");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required");
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to start date");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Reason is required");
            RuleFor(x => x.Reason).MinimumLength(3).WithMessage("Reason must be at least 3 characters long");
        }
    }

    public class SickLeaveUpdateDtoValidator : AbstractValidator<SickLeaveUpdateDto>
    {
        public SickLeaveUpdateDtoValidator()
        {
            Include(new SickLeaveCreateDtoValidator());
            RuleFor(x => x.SickLeaveId).GreaterThan(0).WithMessage("Sick leave ID must be greater than 0");
        }
    }

    public class SickLeaveCompleteDtoValidator : AbstractValidator<SickLeaveCompleteDto>
    {
        public SickLeaveCompleteDtoValidator()
        {
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required");
            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required");
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to start date");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Reason is required");
            RuleFor(x => x.Reason).MinimumLength(3).WithMessage("Reason must be at least 3 characters long");
        }
    }
}
