using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery
{
    public class GetAllCoursesHandler
    : IRequestHandler<GetAllCoursesQuery, Result<IEnumerable<CourseDto>>>
    {
        private readonly ICourseRepository repository;

        public GetAllCoursesHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<IEnumerable<CourseDto>>> Handle(
            GetAllCoursesQuery request,
            CancellationToken cancellationToken)
        {
            var courses = await repository.GetAllCoursesAsync();

            return Result<IEnumerable<CourseDto>>
                .Success(courses,"Courses retrieved successfully.");
        }
    }
}
