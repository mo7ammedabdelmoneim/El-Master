using El_Master.Application.Common.Results;
using MediatR;

namespace El_Master.Application.Features.Lessons.Commands.CreateLessonCommand
{
    public record CreateLessonCommand(Guid CourseId, CreateLessonDto Dto) : IRequest<Result<LessonDetailsDto>>;
}
