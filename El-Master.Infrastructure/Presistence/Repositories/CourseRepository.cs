using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }
        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            return await db.Query("Courses")
                .Select("Id", "Name", "Description", "TeacherId", "GradeId")
                .GetAsync<CourseDto>();
        }

        public async Task<CourseDto?> GetCourseByIdAsync(Guid id)
        {
            return await db.Query("Courses")
                .Where("Id", id)
                .Select("Id", "Name", "Description", "TeacherId", "GradeId")
                .FirstOrDefaultAsync<CourseDto>();
        }
    }
}