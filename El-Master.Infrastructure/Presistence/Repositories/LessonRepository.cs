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
        
        public async Task<List<LessonDetailsDto>> GetPackageLessonsWithAttachmentsAsync(Guid packageId)
        {
            var rows = await db.Query("PackageLessons as pl")
                .Join("Lessons as l", "pl.LessonId", "l.Id")
                .LeftJoin("LessonAttachments as la", "l.Id", "la.LessonId")
                .Where("pl.PackageId", packageId)
                .OrderBy("l.Order")
                .Select(
                    "l.Id as LessonId",
                    "l.Title",
                    "l.Order",
                    "l.DurationInMinutes",
                    "l.VideoPath",

                    "la.Id as AttachmentId",
                    "la.FileName",
                    "la.FilePath"
                )
                .GetAsync<dynamic>();

            var grouped = rows
                .GroupBy(r => (Guid)r.LessonId)
                .Select(g => new LessonDetailsDto
                {
                    Id = g.Key,
                    Title = g.First().Title,
                    Order = g.First().Order,
                    DurationInMinutes = g.First().DurationInMinutes,
                    VideoUrl = g.First().VideoPath, // هنعدلها فوق

                    Attachments = g
                        .Where(x => x.AttachmentId != null)
                        .Select(a => new AttachmentDto
                        {
                            Id = a.AttachmentId,
                            FileName = a.FileName,
                            FileUrl = a.FilePath
                        })
                        .ToList()
                })
                .ToList();

            return grouped;
        }



        public void RemoveAttachments(IEnumerable<LessonAttachment> attachments)
        {
            if (attachments == null || !attachments.Any())
                return;

            context.LessonAttachments.RemoveRange(attachments);
        }

        public async Task<LessonAttachment> GetAttachmentByIdAsync(Guid id)
        {
            return await context.LessonAttachments.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<Lesson>> GetByIdsAsync(List<Guid> ids)
        {
            return await context.Lessons
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public void RemoveAttachment(LessonAttachment attachment)
        {
            context.LessonAttachments.Remove(attachment);
        }

    }
}
