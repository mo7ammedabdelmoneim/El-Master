using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<Grade> GetByNameAsync(string name);
        Task<GradeDto> GetByIdAsync(Guid id);
        Task<IEnumerable<GradeDto>> GetAllGradesAsync();
        Task<IEnumerable<CourseDto>> GetCoursesByGradeAsync(Guid gradeId);
    }
}