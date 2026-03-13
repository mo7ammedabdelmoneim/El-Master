using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<TeacherDto?> GetByIdAsync(Guid id);
        Task<List<TeacherDto>> GetAllTeachersAsync();
    }
}