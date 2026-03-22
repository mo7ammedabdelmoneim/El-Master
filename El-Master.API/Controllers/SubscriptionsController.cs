using El_Master.API.Extensions;
using El_Master.Application.Common.Results;
using El_Master.Application.Features.Subscriptions.Queries.GetMySubscriptionsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SubscriptionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMySubscriptions()
        {
            var studentId = User.GetStudentId();

            if (studentId == null)
                return Unauthorized(new Result<string> { Message = "Students only" });

            var result = await mediator.Send(
                new GetMySubscriptionsQuery(studentId.Value)
            );

            return result.ToApiResponse();
        }
    }
}
