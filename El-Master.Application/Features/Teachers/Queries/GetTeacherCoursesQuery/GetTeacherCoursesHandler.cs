using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.Teachers.Queries.GetTeacherCoursesQuery.GetTeacherCoursesQuery
{
    public class GetTeacherCoursesHandler : IRequestHandler<GetTeacherCourseQuery, Result<IEnumerable<CourseDto>>>
    {
        private readonly ITeacherRepository repository;

        public GetTeacherCoursesHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<IEnumerable<CourseDto>>> Handle(
            GetTeacherCourseQuery request,
            CancellationToken cancellationToken)
        {
            var courses = await repository
                .GetCoursesByTeacherAsync(request.TeacherId);

            return Result<IEnumerable<CourseDto>>
                .Success(courses, "courses of teacher retrived successfully.");
        }
    }
}
