using FluentValidation;

namespace El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand
{
    public class CreatePackageValidator : AbstractValidator<CreatePackageCommand>
    {
        public CreatePackageValidator()
        {
            RuleFor(x => x.Dto)
                 .NotNull()
                 .SetValidator(new CreatePackageDtoValidator());
        }
    }
}
