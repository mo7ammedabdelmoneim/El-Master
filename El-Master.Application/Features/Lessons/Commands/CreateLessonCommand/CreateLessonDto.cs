using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace El_Master.Application.Features.Lessons.Commands.CreateLessonCommand
{
    public class CreateLessonDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Range(1, 500)]
        public int Order { get; set; }

        [Range(1, 1000)]
        public int DurationInMinutes { get; set; }

        [Required]
        public IFormFile Video { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }
}
