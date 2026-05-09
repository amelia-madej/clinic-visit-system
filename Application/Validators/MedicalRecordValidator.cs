using FluentValidation;
using SharedKernel.DTOs;

namespace Application.Validators
{
    public class MedicalRecordCreateDtoValidator : AbstractValidator<MedicalRecordDto>
    {
        public MedicalRecordCreateDtoValidator()
        {
            RuleFor(x => x.VisitId).GreaterThan(0);
            RuleFor(x => x.Interview).NotEmpty().WithMessage("Interview cannot be empty");
            RuleFor(x => x.Diagnosis).NotEmpty().WithMessage("Diagnosis cannot be empty");
            RuleFor(x => x.Recommendations).NotEmpty().WithMessage("Recommendations cannot be empty");
        }
    }

    public class MedicalRecordUpdateDtoValidator : AbstractValidator<MedicalRecordDto>
    {
        public MedicalRecordUpdateDtoValidator()
        {
            Include(new MedicalRecordCreateDtoValidator());
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
