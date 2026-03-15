using El_Master.Application.Features.Courses.Commands.AddCourseCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course Name is required")
                .MaximumLength(50);
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("Teacher Id Name is required");
            RuleFor(x => x.GradeId)
                .NotEmpty().WithMessage("Grade Id Name is required");
        }
    }
}
