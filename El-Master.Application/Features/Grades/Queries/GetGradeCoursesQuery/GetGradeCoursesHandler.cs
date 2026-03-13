using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery
{
    public class GetGradeCoursesHandler : IRequestHandler<GetGradeCoursesQuery, Result<IEnumerable<CourseDto>>>
    {
        private readonly IGradeRepository gradeRepository;

        public GetGradeCoursesHandler(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public async Task<Result<IEnumerable<CourseDto>>> Handle(
            GetGradeCoursesQuery request,
            CancellationToken cancellationToken)
        {
            var courses = await gradeRepository
                .GetCoursesByGradeAsync(request.GradeId);

            return Result<IEnumerable<CourseDto>>
                .Success(courses, "courses of grade retrived successfully.");
        }
    }
}
