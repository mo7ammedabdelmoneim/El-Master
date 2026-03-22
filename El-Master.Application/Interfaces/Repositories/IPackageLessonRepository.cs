using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IPackageLessonRepository : IRepository<PackageLesson>
    {
        Task<List<Lesson>> GetPackageLessonsRawAsync(Guid packageId);
    }
}