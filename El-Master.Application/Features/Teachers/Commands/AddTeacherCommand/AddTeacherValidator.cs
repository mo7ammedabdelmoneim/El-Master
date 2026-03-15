using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.AddTeacherCommand
{
    public class UpdateTeacherValidator : AbstractValidator<AddTeacherCommand>
    {
        public UpdateTeacherValidator()
        {
            RuleFor(x => x.AddTeacherDto)
                .NotNull()
                .SetValidator(new AddTeacherDtoValidator());
        }
    }
}
