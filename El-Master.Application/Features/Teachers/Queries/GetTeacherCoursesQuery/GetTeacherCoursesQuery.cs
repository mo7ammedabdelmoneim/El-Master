using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Queries.GetTeacherCoursesQuery
{
    public record GetTeacherCourseQuery(Guid TeacherId) : IRequest<Result<IEnumerable<CourseDto>>>;
}
