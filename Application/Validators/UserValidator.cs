using FluentValidation;
using SharedKernel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{9}$");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Role)
                .IsInEnum();
        }
    }
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();

            RuleFor(x => x.Role)
                .IsInEnum();
        }
    }

    public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileDto>
    {
        public UpdateUserProfileValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\d{9}$");
        }
    }
}
