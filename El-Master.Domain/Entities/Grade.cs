using System.ComponentModel.DataAnnotations;
namespace El_Master.Domain.Entities
{
    public class Grade : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
