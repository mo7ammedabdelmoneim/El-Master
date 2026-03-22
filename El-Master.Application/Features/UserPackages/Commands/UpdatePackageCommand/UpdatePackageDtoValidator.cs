using El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.UpdatePackageCommand
{
    public class UpdatePackageDtoValidator : AbstractValidator<UpdatePackageDto>
    {
        public UpdatePackageDtoValidator()
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
