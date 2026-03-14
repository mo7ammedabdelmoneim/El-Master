using El_Master.Application.Common.Results;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Queries.GetCourseQuery
{
    public record GetCourseQuery(Guid Id) : IRequest<Result<CourseDto>>;
}
