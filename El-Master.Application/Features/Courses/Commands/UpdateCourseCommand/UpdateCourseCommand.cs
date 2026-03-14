using El_Master.Application.Common.Results;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public record UpdateCourseCommand(Guid Id, UpdateCourseDto Dto) : IRequest<Result<CourseDto>>;
}
