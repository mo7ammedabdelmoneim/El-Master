using FluentValidation;

namespace El_Master.Application.Features.Auth.Commands.GetTokenCommand
{
    public class GetTokenValidator : AbstractValidator<GetTokenCommand>
    {
        public GetTokenValidator()
        {
            RuleFor(x => x.RequestDto)
                .NotNull()
                .SetValidator(new GetTokenDtoValidator());
        }
    }

}