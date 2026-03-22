using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Domain.Entities
{
    public class Package:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;

        [Required]
        public Guid CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public ICollection<PackageLesson> PackageLessons { get; set; }
            = new List<PackageLesson>();

        public ICollection<Subscription> Subscriptions { get; set; }
            = new List<Subscription>();
    }
}

