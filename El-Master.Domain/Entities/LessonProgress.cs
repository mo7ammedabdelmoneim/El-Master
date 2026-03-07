namespace El_Master.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ELearning.Domain.Entities
    {
        public class LessonProgress:BaseEntity
        {
            [Required]
            public Guid LessonId { get; set; }

            [ForeignKey(nameof(LessonId))]
            public Lesson Lesson { get; set; }

            [Required]
            public Guid StudentId { get; set; }

            [ForeignKey(nameof(StudentId))]
            public Student Student { get; set; }

            public bool IsCompleted { get; set; }

            public DateTime? CompletedAt { get; set; }
        }
    }
}
