using El_Master.API.Extensions;
using El_Master.Application.Features.UserPackages.Commands.SubscribeToPackageCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SubscriptionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/api/packages/{packageId}/subscribe")]
        public async Task<IActionResult> Subscribe(Guid packageId)
        {
            var studentId = Guid.Parse(User.FindFirst("studentId")!.Value);

            var command = new SubscribeToPackageCommand(packageId, studentId);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }
    }
}
