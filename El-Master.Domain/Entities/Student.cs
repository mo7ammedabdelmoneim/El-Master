
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Domain.Entities
{
    public class Student:BaseEntity
    {
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public Guid GradeId { get; set; }

        [ForeignKey(nameof(GradeId))]
        public Grade Grade { get; set; }

        [Phone]
        public string? ParentPhone { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
            = new List<Subscription>();
    }
}

