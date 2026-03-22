using El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery;
using El_Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface IPackageRepository : IRepository<Package>
    {
        Task<bool> ExistsAsync(Guid courseId, string name);
        Task<bool> ExistsByIdAsync(Guid courseId);
        Task<List<Guid>> GetExistingLessonIds(Guid packageId, List<Guid> lessonIds);
        Task<PackageLesson?> GetPackageLessonAsync(Guid packageId, Guid lessonId);
        Task<PackageDetailsDto?> GetPackageDetailsAsync(Guid packageId);
        void RemovePackageLesson(PackageLesson entity);
    }
}