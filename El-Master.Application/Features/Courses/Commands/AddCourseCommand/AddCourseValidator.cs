using FluentValidation;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public class UpdateCourseValidator : AbstractValidator<AddCourseCommand>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.AddCourseDto)
                .NotNull()
                .SetValidator(new AddCourseDtoValidator());
        }
    }
}
