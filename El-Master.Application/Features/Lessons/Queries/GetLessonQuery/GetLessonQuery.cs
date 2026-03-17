using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using MediatR;

namespace El_Master.Application.Features.Lessons.Queries.GetLessonQuery
{
    public record GetLessonQuery(Guid Id) : IRequest<Result<LessonDetailsDto>>;
}
