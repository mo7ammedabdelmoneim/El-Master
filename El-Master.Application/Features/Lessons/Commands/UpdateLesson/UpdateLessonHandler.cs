using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace El_Master.Application.Features.Lessons.Commands.UpdateLesson
{
        public class UpdateLessonHandler : IRequestHandler<UpdateLessonCommand, Result<LessonDetailsDto>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonAttachmentRepository lessonAttachmentRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLessonHandler(
            ILessonRepository lessonRepository,
            ILessonAttachmentRepository lessonAttachmentRepository,
            IFileStorageService fileStorage,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            this.lessonAttachmentRepository = lessonAttachmentRepository;
            _fileStorage = fileStorage;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<LessonDetailsDto>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            string? newVideoPath = null;

            try
            {
                var lesson = await _lessonRepository.GetAsync(x=> x.Id == request.LessonId);

                if (lesson == null)
                    return Result<LessonDetailsDto>.Failure("Lesson not found");

                // check order uniqueness inside course
                var orderExists = await _lessonRepository.GetAsync(
                    x => x.CourseId == lesson.CourseId &&
                         x.Order == request.Dto.Order &&
                         x.Id != lesson.Id);

                if (orderExists != null)
                    return Result<LessonDetailsDto>.Failure("Lesson order already exists in this course");

                await _unitOfWork.BeginTransactionAsync();

                // update basic data
                lesson.Title = request.Dto.Title;
                lesson.Order = request.Dto.Order;
                lesson.DurationInMinutes = request.Dto.DurationInMinutes;

                // update video
                if (request.Dto.Video != null)
                {
                    newVideoPath = await _fileStorage.UploadVideoAsync(request.Dto.Video);

                    if (!string.IsNullOrEmpty(lesson.VideoPath))
                        _fileStorage.DeleteFile(lesson.VideoPath);

                    lesson.VideoPath = newVideoPath;
                }

                // add attachments
                if (request.Dto.Attachments != null && request.Dto.Attachments.Any())
                {
                    var attachments = await AddAttachmentsAsync(lesson.Id, request.Dto.Attachments);
                    await lessonAttachmentRepository.AddRangeAsync(attachments);
                }

                await _lessonRepository.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                var dto = _mapper.Map<LessonDetailsDto>(lesson);

                return Result<LessonDetailsDto>.Success(dto, "Lesson updated successfully");
            }
            catch
            {
                await _unitOfWork.RollbackAsync();

                if (newVideoPath != null)
                    _fileStorage.DeleteFile(newVideoPath);

                return Result<LessonDetailsDto>.Failure("Failed to update lesson");
            }
        }

        private async Task<List<LessonAttachment>> AddAttachmentsAsync(Guid lessonId, List<IFormFile> attachments)
        {
            var paths = await _fileStorage.UploadAttachmentsAsync(attachments);
            var LessonAttachment = new List<LessonAttachment>();
            foreach (var (file, path) in attachments.Zip(paths))
            {
                LessonAttachment.Add(new LessonAttachment
                {
                    FileName = file.FileName,
                    FilePath = path,
                    FileType = Path.GetExtension(file.FileName).TrimStart('.'),
                    FileSize = file.Length,
                    LessonId = lessonId,
                });
            }
            return LessonAttachment;
        }
    }
}
    