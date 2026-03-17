using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Application.Interfaces;
using El_Master.Domain.Entities;
using MediatR;
using El_Master.Application.Features.Lessons.Commands.DeleteLessonCommand;

public class DeleteLessonHandler : IRequestHandler<DeleteLessonCommand, Result<string>>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IFileStorageService _fileStorage;

    public DeleteLessonHandler(
        ILessonRepository lessonRepository,
        IFileStorageService fileStorage)
    {
        _lessonRepository = lessonRepository;
        _fileStorage = fileStorage;
    }

    public async Task<Result<string>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = await _lessonRepository.GetLessonWithAttachmentsAsync(request.Id);

            if (lesson == null)
                return Result<string>.Failure("Lesson not found");

            // Delete video
            if (!string.IsNullOrEmpty(lesson.VideoPath))
                _fileStorage.DeleteFile(lesson.VideoPath);

            // Delete attachments
            if (lesson.Attachments != null && lesson.Attachments.Any())
            {
                foreach (var attach in lesson.Attachments)
                {
                    _fileStorage.DeleteFile(attach.FilePath);
                }

                // remove attachments from db
                _lessonRepository.RemoveAttachments(lesson.Attachments);
            }

            // remove lesson
            _lessonRepository.Delete(lesson);

            await _lessonRepository.SaveChangesAsync();
            return Result<string>.Success("Lesson deleted successfully");
        }
        catch
        {
            return Result<string>.Failure("Failed to delete lesson");
        }
    }
}