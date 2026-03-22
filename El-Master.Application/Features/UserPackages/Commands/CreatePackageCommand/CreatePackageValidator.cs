using El_Master.Application.Features.Teachers.Commands.AddTeacherCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
