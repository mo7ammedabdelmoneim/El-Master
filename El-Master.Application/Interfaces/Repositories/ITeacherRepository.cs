using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<TeacherDto?> GetByIdAsync(Guid id);
        Task<List<TeacherDto>> GetAllTeachersAsync();
        Task<TeacherDto?> GetTeacherByIdAsync(Guid id);
        Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId);
    }
}