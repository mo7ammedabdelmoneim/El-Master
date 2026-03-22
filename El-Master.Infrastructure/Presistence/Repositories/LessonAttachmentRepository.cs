
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using SqlKata.Execution;

namespace El_Master.Infrastructure.Presistence.Repositories
{
    public class LessonAttachmentRepository : Repository<LessonAttachment>,ILessonAttachmentRepository
    {
        public LessonAttachmentRepository(ApplicationDbContext context, QueryFactory db) : base(context, db)
        {
        }
    }
}
