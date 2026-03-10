using FluentValidation;

namespace El_Master.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenDtoValidator : AbstractValidator<RevokeTokenDto>
    {
        public RevokeTokenDtoValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required")
                .MaximumLength(50);
        }
    }
}
