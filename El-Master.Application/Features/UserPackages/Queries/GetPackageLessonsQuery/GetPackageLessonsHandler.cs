using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Features.UserPackages.Queries.GetPackageLessonsQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using MediatR;

public class GetPackageLessonsHandler
    : IRequestHandler<GetPackageLessonsQuery, Result<List<LessonDetailsDto>>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IFileStorageService _fileStorage;
    private readonly ILessonRepository lessonRepository;

    public GetPackageLessonsHandler(
        IPackageRepository packageRepository,
        IFileStorageService fileStorage,
        ILessonRepository lessonRepository)
    {
        _packageRepository = packageRepository;
        _fileStorage = fileStorage;
        this.lessonRepository = lessonRepository;
    }

    public async Task<Result<List<LessonDetailsDto>>> Handle(
        GetPackageLessonsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Check package
            var exists = await _packageRepository.ExistsByIdAsync(request.PackageId);
            if (!exists)
                return Result<List<LessonDetailsDto>>.Failure("Package not found");

            // Get data
            var lessons = await lessonRepository
                .GetPackageLessonsWithAttachmentsAsync(request.PackageId);

            //// 3Fix URLs
            //foreach (var lesson in lessons)
            //{
            //    //lesson.VideoUrl = _fileStorage.GetFileUrl(lesson.VideoUrl);

            //    foreach (var att in lesson.Attachments)
            //    {
            //       // att.FileUrl = _fileStorage.GetFileUrl(att.FileUrl);
            //    }
            //}

            return Result<List<LessonDetailsDto>>.Success(lessons, "Lessons retrieved successfully");
        }
        catch
        {
            return Result<List<LessonDetailsDto>>.Failure("Failed to retrieve lessons");
        }
    }
}