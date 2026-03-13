using El_Master.API.Extensions;
using El_Master.Application.Features.Teachers.Commands.AddTeacherCommand;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
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
        public async Task<IActionResult> AddTeacher(AddTeacherDto dto)
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
    }
}
