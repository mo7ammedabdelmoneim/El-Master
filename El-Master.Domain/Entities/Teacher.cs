
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Domain.Entities
{
    public class Teacher:BaseEntity
    { 
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

