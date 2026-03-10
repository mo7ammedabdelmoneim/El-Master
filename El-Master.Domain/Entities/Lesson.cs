
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Domain.Entities
{
    public class Lesson:BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [Url]
        public string VideoUrl { get; set; }

        [Range(1, 500)]
        public int Order { get; set; }

        [Range(1, 1000)]
        public int DurationInMinutes { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public ICollection<PackageLesson> PackageLessons { get; set; }
            = new List<PackageLesson>();
    }
}

