using El_Master.Application.Features.Auth.Commands.RegisterCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenValidator()
        {
            RuleFor(x => x.RevokeTokenDto)
                .NotNull()
                .SetValidator(new RevokeTokenDtoValidator());
        }
    }
}
