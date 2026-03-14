using El_Master.Application.Common.Results;
using MediatR;

namespace El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery
{
    public record GetAllCoursesQuery() : IRequest<Result<IEnumerable<CourseDto>>>;
}
