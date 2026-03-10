using El_Master.Application.Features.Auth.Commands.GetTokenCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.RegisterCommand
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.RegisterDto)
                .NotNull()
                .SetValidator(new RegisterDtoValidator());
        }

    }
}
