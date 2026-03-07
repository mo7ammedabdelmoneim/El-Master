using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    namespace ELearning.Domain.Entities
    {
        public class Teacher:BaseEntity
        { 
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [MaxLength(500)]
            public string? Bio { get; set; }

            [Url]
            public string? ImageUrl { get; set; }

            public ICollection<Course> Courses { get; set; } = new List<Course>();
        }
    }
}
