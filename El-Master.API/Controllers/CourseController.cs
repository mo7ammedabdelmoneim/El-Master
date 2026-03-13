using El_Master.API.Extensions;
using El_Master.Application.Features.Courses.Commands.AddCourseCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator mediator;

        public CourseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(AddCourseDto courseDto)
        {
            var command = new AddCourseCommand(courseDto);
            var result = await mediator.Send(command);
            return result.ToApiResponse();
        }
    }
}
