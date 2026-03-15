using FluentValidation;

namespace El_Master.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseValidator :AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseValidator ()
        {
            RuleFor(x => x.Dto)
                .NotNull()
                .SetValidator(new UpdateCourseDtoValidator());
        }
    }
}
