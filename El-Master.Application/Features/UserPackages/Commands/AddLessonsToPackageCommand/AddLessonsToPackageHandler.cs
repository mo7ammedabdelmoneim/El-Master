using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.AddLessonsToPackageCommand
{
    public class AddLessonsToPackageHandler
    : IRequestHandler<AddLessonsToPackageCommand, Result<bool>>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IPackageLessonRepository packageLessonRepository;

        public AddLessonsToPackageHandler(
            IPackageRepository packageRepository,
            ILessonRepository lessonRepository,
            IPackageLessonRepository packageLessonRepository)
        {
            _packageRepository = packageRepository;
            _lessonRepository = lessonRepository;
            this.packageLessonRepository = packageLessonRepository;
        }

        public async Task<Result<bool>> Handle(
            AddLessonsToPackageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                if(request.Dto.LessonIds == null || !request.Dto.LessonIds.Any())
                    return Result<bool>.Failure("LessonIds is required");

                var package = await _packageRepository.GetAsync(x=>x.Id == request.PackageId);
                if (package == null)
                    return Result<bool>.Failure("Package not found");

                var lessons = await _lessonRepository.GetByIdsAsync(request.Dto.LessonIds);

                if (!lessons.Any())
                    return Result<bool>.Failure("No valid lessons found");

                // Validate same course
                if (lessons.Any(l => l.CourseId != package.CourseId))
                    return Result<bool>.Failure("All lessons must belong to the same course as the package");

                // Remove duplicates (already added)
                var existingIds = await _packageRepository
                    .GetExistingLessonIds(request.PackageId, request.Dto.LessonIds);

                var newLessons = lessons
                    .Where(l => !existingIds.Contains(l.Id))
                    .ToList();

                if (!newLessons.Any())
                    return Result<bool>.Failure("All lessons already added");

                // Add relations
                var packageLessons = new List<PackageLesson>();
                foreach (var lesson in newLessons)
                {
                    packageLessons.Add(new PackageLesson
                    {
                        Id = Guid.NewGuid(),
                        PackageId = package.Id,
                        LessonId = lesson.Id
                    });
                }

                await packageLessonRepository.AddRangeAsync(packageLessons);
                await _packageRepository.SaveChangesAsync();

                return Result<bool>.Success(true, "Lessons added to package successfully");
            }
            catch
            {
                return Result<bool>.Failure("Failed to add lessons to package");
            }
        }
    }
}
