using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.AddTeacherCommand
{
    public class AddTeacherDtoValidator : AbstractValidator<AddTeacherDto>
    {
        public AddTeacherDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6)
                .MaximumLength(50)
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number");
        }
    }
}