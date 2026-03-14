using El_Master.API.Extensions;
using El_Master.Application.Features.Courses.Commands.AddCourseCommand;
using El_Master.Application.Features.Courses.Commands.DeleteCourseCommand;
using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Features.Courses.Queries.GetCourseQuery;
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

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var result = await mediator.Send(new GetAllCoursesQuery());
            return result.ToApiResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            var result = await mediator.Send(new GetCourseQuery(id));
            return result.ToApiResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, UpdateCourseDto dto)
        {
            var result = await mediator.Send(new UpdateCourseCommand(id, dto));
            return result.ToApiResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await mediator.Send(new DeleteCourseCommand(id));
            return result.ToApiResponse();
        }
    }
}
