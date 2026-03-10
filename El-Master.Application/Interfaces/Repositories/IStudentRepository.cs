using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        //Task<Grade?> GetByNameAsync(string name);
    }
}