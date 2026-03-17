using FluentValidation;

namespace El_Master.Application.Features.Lessons.Commands.CreateLessonCommand
{
    public class CreateLessonDtoValidator : AbstractValidator<CreateLessonDto>
    {
        public CreateLessonDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Lesson Title is required").MaximumLength(200);
            RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0");
            RuleFor(x => x.DurationInMinutes).GreaterThan(0).WithMessage("Duration must be greater than 0");
            RuleFor(x => x.Video).NotNull().WithMessage("Video file is required");
        }
    }
}
