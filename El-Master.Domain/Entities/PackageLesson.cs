
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Domain.Entities
{
    public class PackageLesson:BaseEntity
    {
        [Required]
        public Guid PackageId { get; set; }

        [ForeignKey(nameof(PackageId))]
        public Package Package { get; set; }

        [Required]
        public Guid LessonId { get; set; }

        [ForeignKey(nameof(LessonId))]
        public Lesson Lesson { get; set; }
    }

}

