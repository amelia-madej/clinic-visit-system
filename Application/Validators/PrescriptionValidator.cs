using FluentValidation;
using Application.DTOs;

namespace Application.Validators
{
    public class PrescriptionItemCreateDtoValidator : AbstractValidator<PrescriptionItemCreateDto>
    {
        public PrescriptionItemCreateDtoValidator()
        {
            RuleFor(x => x.MedicationId).GreaterThan(0);
            RuleFor(x => x.Dosage).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Instructions).NotEmpty();
        }
    }

    public class PrescriptionCreateDtoValidator : AbstractValidator<PrescriptionCreateDto>
    {
        public PrescriptionCreateDtoValidator()
        {
            RuleForEach(x => x.Items).SetValidator(new PrescriptionItemCreateDtoValidator());
        }
    }
}
