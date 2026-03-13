using El_Master.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public class AddCourseDto
    {
        public string CourseName { get; set; }
        public string? Description { get; set; }
        public Guid TeacherId { get; set; }
        public Guid GradeId { get; set; }
    }
}
