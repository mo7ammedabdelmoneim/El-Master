using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Queries.GetPackageLessonsQuery
{
    public class PackageLessonDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public int DurationInMinutes { get; set; }

        public string VideoUrl { get; set; }
    }
}
