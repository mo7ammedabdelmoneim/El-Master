namespace El_Master.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ELearning.Domain.Entities
    {
        public class Subscription:BaseEntity
        { 
            [Required]
            public Guid StudentId { get; set; }

            [ForeignKey(nameof(StudentId))]
            public Student Student { get; set; }

            [Required]
            public Guid PackageId { get; set; }

            [ForeignKey(nameof(PackageId))]
            public Package Package { get; set; }

            [Required]
            public DateTime StartDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }

            public bool IsActive { get; set; }
        }
    }
}
