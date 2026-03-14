using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context, QueryFactory db) : base(context,db)
        {
        }

        public async Task<List<TeacherDto>> GetAllTeachersAsync()
        {
           var result = await db.Query("Teachers as t")
                                     .LeftJoin("Courses as c", "t.Id", "c.TeacherId")
                                     .Select("t.Id", "t.FirstName", "t.LastName","t.Bio", "t.ImageUrl")
                                     .SelectRaw("COUNT(c.Id) as CoursesCount")
                                     .GroupBy("t.Id", "t.FirstName", "t.LastName", "t.Bio", "t.ImageUrl")
                                     .GetAsync<TeacherDto>();
            return result.ToList();
        }

        public async Task<TeacherDto?> GetByIdAsync(Guid id)
        {
            var result = await db.Query("Teachers as t")
                .LeftJoin("Courses as c", "t.Id", "c.TeacherId")
                .Where("t.Id", id)
                .Select("t.Id", "t.FirstName", "t.LastName", "t.Bio", "t.ImageUrl")
                .SelectRaw("COUNT(c.Id) as CoursesCount")
                .GroupBy("t.Id", "t.FirstName", "t.LastName", "t.Bio", "t.ImageUrl")
                .FirstOrDefaultAsync<TeacherDto>();

            return result;
        }

        public async Task<TeacherDto?> GetTeacherByIdAsync(Guid id)
        {
            return await db.Query("Teachers as t")
                .LeftJoin("Courses as c", "t.Id", "c.TeacherId")
                .Where("t.Id", id)
                .Select(
                    "t.Id",
                    "t.FirstName",
                    "t.LastName",
                    "t.Bio",
                    "t.ImageUrl"
                )
                .SelectRaw("COUNT(c.Id) as CoursesCount")
                .GroupBy(
                    "t.Id",
                    "t.FirstName",
                    "t.LastName",
                    "t.Bio",
                    "t.ImageUrl"
                )
                .FirstOrDefaultAsync<TeacherDto>();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId)
        {
            return await db.Query("Courses")
                .Where("TeacherId", teacherId)
                .Select("Id", "Name")
                .GetAsync<CourseDto>();
        }
    }
}
