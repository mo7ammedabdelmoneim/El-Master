using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.UserPackages.Commands.RemoveLessonFromPackageCommand
{
    public class RemoveLessonFromPackageHandler
        : IRequestHandler<RemoveLessonFromPackageCommand, Result<bool>>
    {
        private readonly IPackageRepository _packageRepository;

        public RemoveLessonFromPackageHandler(
            IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Result<bool>> Handle(
            RemoveLessonFromPackageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var relation = await _packageRepository.GetPackageLessonAsync(request.PackageId, request.LessonId);

                if (relation == null)
                    return Result<bool>.Failure("Lesson not found in this package");

                _packageRepository.RemovePackageLesson(relation);

                await _packageRepository.SaveChangesAsync();

                return Result<bool>.Success(true, "Lesson removed from package");
            }
            catch
            {
                return Result<bool>.Failure("Failed to remove lesson");
            }
        }
    }
}
