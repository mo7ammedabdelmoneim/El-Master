using El_Master.API.Extensions;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Features.Grades.Queries.GetGradeCoursesQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IMediator mediator;

        public GradeController(IMediator mediator)
        {
            this.mediator = mediator;
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
