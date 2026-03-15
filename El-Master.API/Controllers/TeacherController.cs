using El_Master.API.Extensions;
using El_Master.Application.Features.Teachers.Commands.AddTeacherCommand;
using El_Master.Application.Features.Teachers.Commands.DeleteTeacherCommand;
using El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Application.Features.Teachers.Queries.GetTeacherCoursesQuery;
using El_Master.Application.Features.Teachers.Queries.GetTeacherQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator mediator;

        public TeacherController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromForm]AddTeacherDto dto)
        {
            var result = await mediator.Send(new AddTeacherCommand(dto));

            return result.ToApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await mediator.Send(new GetAllTeachersQuery());
            return result.ToApiResponse();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(Guid id)
        {
            var result = await mediator.Send(new GetTeacherQuery(id));
            return result.ToApiResponse();
        }

        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetTeacherCourses(Guid id)
        {
            var result = await mediator.Send(new GetTeacherCourseQuery(id));
            return result.ToApiResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(Guid id, UpdateTeacherDto dto)
        {
            var result = await mediator.Send(new UpdateTeacherCommand(id, dto));
            return result.ToApiResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var result = await mediator.Send(new DeleteTeacherCommand(id));
            return result.ToApiResponse();
        }
    }
}
