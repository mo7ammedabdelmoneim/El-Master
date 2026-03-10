
using El_Master.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Domain.Entities
{
    public class Package:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        public PackageType Type { get; set; }

        //[Range(1, 365)]
        //public int DurationInDays { get; set; }

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

