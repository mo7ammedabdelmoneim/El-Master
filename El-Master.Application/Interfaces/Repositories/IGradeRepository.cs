using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<Grade?> GetByNameAsync(string name);
    }
}