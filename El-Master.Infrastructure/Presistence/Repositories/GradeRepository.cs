using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class GradeRepository : Repository<Grade>, IGradeRepository
    {
        public GradeRepository(ICommandRepository<Grade> command, IQueryRepository<Grade> query) : base(command, query)
        {
        }

        public async Task<Grade?> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM Grades WHERE LOWER(Name) = @Name";
            return await Query.GetAsync(sql, new { Name = name.ToLower() });
        }
    }
}
