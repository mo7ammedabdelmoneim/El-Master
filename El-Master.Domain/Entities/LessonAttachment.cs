using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace El_Master.Domain.Entities
{
    public class LessonAttachment : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [MaxLength(50)]
        public string FileType { get; set; }

        public long FileSize { get; set; }

        [Required]
        public Guid LessonId { get; set; }

        [ForeignKey(nameof(LessonId))]
        public Lesson Lesson { get; set; }
    }
}