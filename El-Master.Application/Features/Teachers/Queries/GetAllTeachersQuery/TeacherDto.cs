using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery
{
    public class TeacherDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public int CoursesCount { get; set; }
    }
}
