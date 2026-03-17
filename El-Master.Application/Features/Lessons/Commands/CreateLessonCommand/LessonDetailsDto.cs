using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Lessons.Commands.CreateLessonCommand
{
    public class LessonDetailsDto
    {
        public Guid Id { get; set; }          
        public string Title { get; set; }     
        public int Order { get; set; }        
        public int DurationInMinutes { get; set; } 
        public string VideoUrl { get; set; }     
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>(); 
    }

    public class AttachmentDto
    {
        public Guid Id { get; set; }           
        public string FileName { get; set; }   
        public string FileUrl { get; set; }    
    }
}
