namespace El_Master.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    namespace ELearning.Domain.Entities
    {
        public class Grade:BaseEntity
        {
            [Required]
            [MaxLength(50)]
            public string Name { get; set; }

            public ICollection<Course> Courses { get; set; } = new List<Course>();
        }
    }
}
