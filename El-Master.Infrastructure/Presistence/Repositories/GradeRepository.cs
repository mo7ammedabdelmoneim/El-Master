using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }

        public async Task<GradeDto> GetByIdAsync(Guid id)
        {
            return await db.Query("Grades")
                .Where("Id", id)
                .Select("Id", "Name")
                .FirstOrDefaultAsync<GradeDto>();
        }

        public async Task<Grade> GetByNameAsync(string name)
        {
            return await db.Query("Grades")
                .Where("Name", name)
                .FirstOrDefaultAsync<Grade>();
        }
        public async Task<IEnumerable<GradeDto>> GetAllGradesAsync()
        {
            return await db.Query("Grades")
                .Select("Id", "Name")
                .GetAsync<GradeDto>();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByGradeAsync(Guid gradeId)
        {
            return await db.Query("Courses")
                .Where("GradeId", gradeId)
                .Select("Id", "Name")
                .GetAsync<CourseDto>();
        }
    }
}