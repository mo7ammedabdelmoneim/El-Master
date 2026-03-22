using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class PackageLessonRepository : Repository<PackageLesson>, IPackageLessonRepository
    {
        public PackageLessonRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }
        public async Task<List<Lesson>> GetPackageLessonsRawAsync(Guid packageId)
        {
            var lessons = await db.Query("PackageLessons as pl")
                .Join("Lessons as l", "pl.LessonId", "l.Id")
                .Where("pl.PackageId", packageId)
                .OrderBy("l.Order")
                .Select(
                    "l.Id",
                    "l.Title",
                    "l.Order",
                    "l.DurationInMinutes",
                    "l.VideoPath"
                )
                .GetAsync<Lesson>();

            return lessons.ToList();
        }
    }
}
