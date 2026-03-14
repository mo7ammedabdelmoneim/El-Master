using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid TeacherId { get; set; }
        public Guid GradeId { get; set; }
    }
}
