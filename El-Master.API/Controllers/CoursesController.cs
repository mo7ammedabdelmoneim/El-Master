using El_Master.API.Extensions;
using El_Master.Application.Features.Courses.Commands.AddCourseCommand;
using El_Master.Application.Features.Courses.Commands.DeleteCourseCommand;
using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Features.Courses.Queries.GetCourseQuery;
using El_Master.Application.Features.Lessons.Queries.GetCourseLessonsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CoursesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
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

        [Authorize]
        [HttpGet("{id}/lessons")]
        public async Task<IActionResult> GetCourseLessons(Guid id)
        {
            var query = new GetCourseLessonsQuery(id);

            var result = await mediator.Send(query);

            return result.ToApiResponse();
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(Guid id, UpdateCourseDto dto)
        {
            var result = await mediator.Send(new UpdateCourseCommand(id, dto));
            return result.ToApiResponse();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await mediator.Send(new DeleteCourseCommand(id));
            return result.ToApiResponse();
        }
    }
}
