using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Domain.Entities;

namespace El_Master.Application.Interfaces.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<Lesson> GetLessonWithAttachmentsAsync(Guid lessonId);
        Task<Lesson?> GetLessonAsync(Guid lessonId);
        Task<List<LessonDetailsDto>> GetLessonsByCourseIdAsync(Guid courseId);
        void RemoveAttachments(IEnumerable<LessonAttachment> attachments);
        Task<LessonAttachment> GetAttachmentByIdAsync(Guid id);

        void RemoveAttachment(LessonAttachment attachment);
    }
}