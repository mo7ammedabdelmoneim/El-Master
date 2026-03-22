using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.AddLessonsToPackageCommand
{
    public class AddLessonsToPackageDto
    {
        public List<Guid> LessonIds { get; set; } = new();
    }
}
