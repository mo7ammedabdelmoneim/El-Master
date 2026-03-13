using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.AddTeacherCommand
{
    public class AddTeacherValidator : AbstractValidator<AddTeacherCommand>
    {
        public AddTeacherValidator()
        {
            RuleFor(x => x.AddTeacherDto)
                .NotNull()
                .SetValidator(new AddTeacherDtoValidator());
        }
    }
}
