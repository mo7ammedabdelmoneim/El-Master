using El_Master.API.Extensions;
using El_Master.Application.Features.UserPackages.Commands.AddLessonsToPackageCommand;
using El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand;
using El_Master.Application.Features.UserPackages.Commands.RemoveLessonFromPackageCommand;
using El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery;
using El_Master.Application.Features.UserPackages.Queries.GetPackageLessonsQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PackagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("{courseId}")]
        public async Task<IActionResult> CreatePackage(Guid courseId, CreatePackageDto dto)
        {
            var command = new CreatePackageCommand(courseId, dto);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [HttpGet("{packageId}")]
        public async Task<IActionResult> GetPackage(Guid packageId)
        {
            var result = await mediator.Send(new GetPackageDetailsQuery(packageId));

            return result.ToApiResponse();
        }

        [HttpPost("{packageId}/lessons")]
        public async Task<IActionResult> AddLessons(Guid packageId, AddLessonsToPackageDto dto)
        {
            var command = new AddLessonsToPackageCommand(packageId, dto);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [HttpGet("/api/packages/{packageId}/lessons")]
        public async Task<IActionResult> GetLessons(Guid packageId)
        {
            var result = await mediator.Send(new GetPackageLessonsQuery(packageId));

            return result.ToApiResponse();
        }

        [HttpDelete("{packageId}/lessons/{lessonId}")]
        public async Task<IActionResult> RemoveLesson(Guid packageId, Guid lessonId)
        {
            var command = new RemoveLessonFromPackageCommand(packageId, lessonId);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }


    }
}
