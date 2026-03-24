using El_Master.API.Extensions;
using El_Master.Application.Common.Results;
using El_Master.Application.Features.UserPackages.Commands.AddLessonsToPackageCommand;
using El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand;
using El_Master.Application.Features.UserPackages.Commands.RemoveLessonFromPackageCommand;
using El_Master.Application.Features.UserPackages.Commands.SubscribeToPackageCommand;
using El_Master.Application.Features.UserPackages.Commands.UpdatePackageCommand;
using El_Master.Application.Features.UserPackages.Queries.GetAllPackagesQuery;
using El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery;
using El_Master.Application.Features.UserPackages.Queries.GetPackageLessonsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost("{courseId}")]
        public async Task<IActionResult> CreatePackage(Guid courseId, CreatePackageDto dto)
        {
            var command = new CreatePackageCommand(courseId, dto);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllPackagesQuery());

            return result.ToApiResponse();
        }

        [HttpGet("{packageId}")]
        public async Task<IActionResult> GetPackage(Guid packageId)
        {
            var result = await mediator.Send(new GetPackageDetailsQuery(packageId));

            return result.ToApiResponse();
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPut("{packageId}")]
        public async Task<IActionResult> Update(Guid packageId, UpdatePackageDto dto)
        {
            var command = new UpdatePackageCommand(packageId, dto);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost("{packageId}/lessons")]
        public async Task<IActionResult> AddLessons(Guid packageId, AddLessonsToPackageDto dto)
        {
            var command = new AddLessonsToPackageCommand(packageId, dto);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [HttpGet("{packageId}/lessons")]
        public async Task<IActionResult> GetLessons(Guid packageId)
        {
            var result = await mediator.Send(new GetPackageLessonsQuery(packageId));

            return result.ToApiResponse();
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpDelete("{packageId}/lessons/{lessonId}")]
        public async Task<IActionResult> RemoveLesson(Guid packageId, Guid lessonId)
        {
            var command = new RemoveLessonFromPackageCommand(packageId, lessonId);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }

        [Authorize]
        [HttpPost("{packageId}/subscribe")]
        public async Task<IActionResult> Subscribe(Guid packageId)
        {
            var studentId = User.GetStudentId();

            if (studentId == null)
                return Unauthorized(new Result<string> { Message = "Students only" });

            var command = new SubscribeToPackageCommand(packageId, studentId.Value);

            var result = await mediator.Send(command);

            return result.ToApiResponse();
        }
    }
}
