using El_Master.Application.Features.Teachers.Commands.AddTeacherCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.UpdatePackageCommand
{
    public class UpdatePackageValidator : AbstractValidator<UpdatePackageCommand>
    {
        public UpdatePackageValidator()
        {
            RuleFor(x => x.Dto)
                 .NotNull()
                 .SetValidator(new UpdatePackageDtoValidator());
        }
    }
}
