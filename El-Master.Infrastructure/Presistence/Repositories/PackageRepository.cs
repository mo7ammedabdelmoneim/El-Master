using El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        public PackageRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }
        public async Task<bool> ExistsAsync(Guid courseId, string name)
        {
            var result = await db.Query("Packages")
                .Where("CourseId", courseId)
                .Where("Name", name)
                .FirstOrDefaultAsync();

            return result != null;
        }
        
        public async Task<bool> ExistsByIdAsync(Guid packageId)
        {
            var result = await db.Query("Packages")
                .Where("Id", packageId)
                .FirstOrDefaultAsync();

            return result != null;
        }
        public async Task<List<Guid>> GetExistingLessonIds(Guid packageId, List<Guid> lessonIds)
        {
            return await context.PackageLessons
                .Where(x => x.PackageId == packageId && lessonIds.Contains(x.LessonId))
                .Select(x => x.LessonId)
                .ToListAsync();
        }

        public async Task<PackageLesson?> GetPackageLessonAsync(Guid packageId, Guid lessonId)
        {
            return await context.PackageLessons
                .FirstOrDefaultAsync(x => x.PackageId == packageId && x.LessonId == lessonId);
        }

        public async Task<PackageDetailsDto?> GetPackageDetailsAsync(Guid packageId)
        {
            var package = await db.Query("Packages as p")
                .Where("p.Id", packageId)
                .Select(
                    "p.Id",
                    "p.Name",
                    "p.Description",
                    "p.Price",
                    "p.Order",
                    "p.IsActive"
                )
                .FirstOrDefaultAsync<PackageDetailsDto>();

            if (package == null)
                return null;

            var count = await db.Query("PackageLessons")
                .Where("PackageId", packageId)
                .CountAsync<int>();

            package.LessonsCount = count;

            return package;
        }


        public void RemovePackageLesson(PackageLesson entity)
        {
            context.PackageLessons.Remove(entity);
        }
    }
}
