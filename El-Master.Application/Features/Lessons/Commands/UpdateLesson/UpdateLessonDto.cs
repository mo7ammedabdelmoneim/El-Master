using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonDto
    {
        public string Title { get; set; }
        public int Order { get; set; }
        public int DurationInMinutes { get; set; }

        public IFormFile? Video { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }
}
