using El_Master.Application.Common.Results;
using MediatR;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public record AddCourseCommand(AddCourseDto AddCourseDto) : IRequest<Result<AddCourseDto>>
    {
    }
}
