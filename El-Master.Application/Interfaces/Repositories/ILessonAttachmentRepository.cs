using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface ILessonAttachmentRepository : IRepository<LessonAttachment>
    {
        Task AddRange(List<LessonAttachment> attachments);
    }
}