using El_Master.Application.Features.Subscriptions.Queries.GetMySubscriptionsQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {

        }
        public async Task<bool> ExistsAsync(Guid studentId, Guid packageId)
        {
            return await context.Subscriptions
                .AnyAsync(x => x.StudentId == studentId && x.PackageId == packageId);
        }

        public async Task<List<MySubscriptionDto>> GetStudentSubscriptionsAsync(Guid studentId)
        {
            var result = await db.Query("Subscriptions as s")
                .Join("Packages as p", "s.PackageId", "p.Id")
                .Join("Courses as c", "p.CourseId", "c.Id")
                .Where("s.StudentId", studentId)
                .Select(
                    "s.Id as SubscriptionId",
                    "s.StartDate",
                    "s.IsActive",

                    "p.Id as PackageId",
                    "p.Name as PackageName",
                    "p.Price",

                    "c.Id as CourseId",
                    "c.Name as CourseName"
                )
                .OrderByDesc("s.StartDate")
                .GetAsync<MySubscriptionDto>();

            return result.ToList();
        }

        public async Task<bool> HasAccessToLessonAsync(Guid studentId, Guid lessonId)
{
    var result = await db.Query("Subscriptions as s")
        .Join("Packages as p", "s.PackageId", "p.Id")
        .Join("PackageLessons as pl", "p.Id", "pl.PackageId")
        .Where("s.StudentId", studentId)
        .Where("s.IsActive", true)
        .Where("pl.LessonId", lessonId)
        .Select("s.Id")
        .FirstOrDefaultAsync();

    return result != null;
}
    } 
}