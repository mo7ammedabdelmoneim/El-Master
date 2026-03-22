using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand
{
    public class CreatePackageDtoValidator : AbstractValidator<CreatePackageDto>
    {
        public CreatePackageDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Order)
                .GreaterThan(0);
        }
    }
}
