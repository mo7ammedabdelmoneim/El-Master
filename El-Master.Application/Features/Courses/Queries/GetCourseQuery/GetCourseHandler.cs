using El_Master.Application.Common.Results;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Queries.GetCourseQuery
{
    public class GetCourseHandler
     : IRequestHandler<GetCourseQuery, Result<CourseDto>>
    {
        private readonly ICourseRepository repository;

        public GetCourseHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<CourseDto>> Handle(
            GetCourseQuery request,
            CancellationToken cancellationToken)
        {
            var course = await repository.GetCourseByIdAsync(request.Id);

            if (course == null)
                return Result<CourseDto>.Failure("Course not found");

            return Result<CourseDto>.Success(course, "Course retrieved successfully.");
        }
    }
}
