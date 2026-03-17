using El_Master.API.Extensions;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Features.Lessons.Commands.DeleteAttachmentCommand;
using El_Master.Application.Features.Lessons.Commands.DeleteLessonCommand;
using El_Master.Application.Features.Lessons.Commands.UpdateLesson;
using El_Master.Application.Features.Lessons.Commands.UploadLessonAttachmentCommand;
using El_Master.Application.Features.Lessons.Queries.GetCourseLessonsQuery;
using El_Master.Application.Features.Lessons.Queries.GetLessonQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LessonsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{courseId}")]
        public async Task<IActionResult> CreateLesson(Guid courseId, [FromForm] CreateLessonDto lessonDto)
        {
            var command = new CreateLessonCommand(courseId, lessonDto);
            var result = await mediator.Send(command);
            return result.ToApiResponse();
        }

        [HttpPut("{lessonId}")]
        public async Task<IActionResult> UpdateLesson(Guid lessonId, [FromForm] UpdateLessonDto dto)
        {
            var command = new UpdateLessonCommand(lessonId, dto);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLesson(Guid id)
        {
            var query = new GetLessonQuery(id);

            var result = await mediator.Send(query);

            return result.ToApiResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var command = new DeleteLessonCommand(id);
            var result = await mediator.Send(command);
            return result.ToApiResponse();
        }

        [HttpPost("{id}/attachments")]
        public async Task<IActionResult> UploadLessonAttachments(Guid id, [FromForm] List<IFormFile> attachments)
        {
            var command = new UploadLessonAttachmentsCommand(id, attachments);
            var result = await mediator.Send(command);
            return result.ToApiResponse();
        }

        [HttpDelete("/api/attachments/{id}")]
        public async Task<IActionResult> DeleteAttachment(Guid id)
        {
            var command = new DeleteAttachmentCommand(id);
            var result = await mediator.Send(command);
            return result.ToApiResponse();
        }
    }
}
