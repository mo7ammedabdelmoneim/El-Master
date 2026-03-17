using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }
        public async Task<Lesson?> GetLessonWithAttachmentsAsync(Guid lessonId)
        {
            var lesson = await db.Query("Lessons")
                .Where("Id", lessonId)
                .FirstOrDefaultAsync<Lesson>();

            if (lesson == null)
                return null;

            var attachments = await db.Query("LessonAttachments")
                .Where("LessonId", lessonId)
                .GetAsync<LessonAttachment>();

            lesson.Attachments = attachments.ToList();

            return lesson;
        }
        
        public async Task<Lesson?> GetLessonAsync(Guid lessonId)
        {
            var lesson = await context.Lessons.Include(x=>x.Attachments).FirstOrDefaultAsync(x => x.Id == lessonId);
            return lesson;
        }

        public async Task<List<LessonDetailsDto>> GetLessonsByCourseIdAsync(Guid courseId)
        {
            var lessons = await db.Query("Lessons")
                .Where("CourseId", courseId)
                .OrderBy("Order")
                .GetAsync<Lesson>();

            if (!lessons.Any())
                return new List<LessonDetailsDto>();


            var attachments = await db.Query("LessonAttachments")
                .WhereIn("LessonId", lessons.Select(x => x.Id))
                .GetAsync<LessonAttachment>();


            var result = lessons.Select(l => new LessonDetailsDto
            {
                Id = l.Id,
                Title = l.Title,
                Order = l.Order,
                DurationInMinutes = l.DurationInMinutes,
                VideoUrl = l.VideoPath,

                Attachments = attachments
                    .Where(a => a.LessonId == l.Id)
                    .Select(a => new AttachmentDto
                    {
                        Id = a.Id,
                        FileName = a.FileName,
                        FileUrl = a.FilePath
                    }).ToList()

            }).ToList();

            return result;
        }

        public void RemoveAttachments(IEnumerable<LessonAttachment> attachments)
        {
            if (attachments == null || !attachments.Any())
                return;

            context.lessonAttachments.RemoveRange(attachments);
        }

        public async Task<LessonAttachment> GetAttachmentByIdAsync(Guid id)
        {
            return await context.lessonAttachments.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public void RemoveAttachment(LessonAttachment attachment)
        {
            context.lessonAttachments.Remove(attachment);
        }

    }
}
