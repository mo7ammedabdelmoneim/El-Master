using El_Master.Application.Common.Results;
using MediatR;

namespace El_Master.Application.Features.Lessons.Commands.DeleteLessonCommand
{
    public record DeleteLessonCommand(Guid Id) : IRequest<Result<string>>;
}
