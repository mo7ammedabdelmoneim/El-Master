using FluentValidation;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public class AddCourseDtoValidator : AbstractValidator<AddCourseDto>
    {
        public AddCourseDtoValidator()
        {
            RuleFor(x => x.CourseName)
                .NotEmpty().WithMessage("Course Name is required")
                .MaximumLength(50);
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("Teacher Id Name is required");
            RuleFor(x => x.GradeId)
                .NotEmpty().WithMessage("Grade Id Name is required");
        }
    }
}
