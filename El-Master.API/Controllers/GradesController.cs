using El_Master.API.Extensions;
using El_Master.Application.Features.Grades.Commands.AddGradeCommand;
using El_Master.Application.Features.Grades.Commands.DeleteGradeCommand;
using El_Master.Application.Features.Grades.Commands.UpdateGradeCommand;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using El_Master.Application.Features.Grades.Queries.GetGradeQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IMediator mediator;

        public GradesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddGrade(AddGradeDto dto)
        {
            var result = await mediator.Send(new AddGradeCommand(dto));
            return result.ToApiResponse();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(Guid id)
        {
            var result = await mediator.Send(new GetGradeQuery(id));
            return result.ToApiResponse();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrade(Guid id, UpdateGradeDto dto)
        {
            var result = await mediator.Send(new UpdateGradeCommand(id, dto));
            return result.ToApiResponse();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var result = await mediator.Send(new DeleteGradeCommand(id));
            return result.ToApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var result = await mediator.Send(new GetAllGradesQuery());
            return result.ToApiResponse();
        }

        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetCourses(Guid id)
        {
            var result = await mediator.Send(new GetGradeCoursesQuery(id));
            return result.ToApiResponse();
        }
    }
}
