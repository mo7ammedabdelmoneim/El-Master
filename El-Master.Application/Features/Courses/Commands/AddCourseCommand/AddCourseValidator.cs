using El_Master.Application.Features.Auth.Commands.RevokeToken;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public class AddCourseValidator : AbstractValidator<AddCourseCommand>
    {
        public AddCourseValidator()
        {
            RuleFor(x => x.AddCourseDto)
                .NotNull()
                .SetValidator(new AddCourseDtoValidator());
        }
    }
}
