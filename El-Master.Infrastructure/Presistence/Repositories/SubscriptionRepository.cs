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
    } 
}