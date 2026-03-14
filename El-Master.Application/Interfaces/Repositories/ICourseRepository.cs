using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(Guid id);
    }
}