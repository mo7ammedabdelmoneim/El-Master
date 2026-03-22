using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<bool> ExistsAsync(Guid studentId, Guid packageId);

    }
}