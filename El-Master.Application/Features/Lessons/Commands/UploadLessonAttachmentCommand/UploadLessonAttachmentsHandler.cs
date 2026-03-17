using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Application.Interfaces;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;

namespace El_Master.Application.Features.Lessons.Commands.UploadLessonAttachmentCommand
{
    public class UploadLessonAttachmentsHandler
     : IRequestHandler<UploadLessonAttachmentsCommand, Result<LessonDetailsDto>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonAttachmentRepository lessonAttachmentRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper mapper;

        public UploadLessonAttachmentsHandler(
            ILessonRepository lessonRepository,
            ILessonAttachmentRepository lessonAttachmentRepository,
            IFileStorageService fileStorage,
            IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            this.lessonAttachmentRepository = lessonAttachmentRepository;
            _fileStorage = fileStorage;
            this.mapper = mapper;
        }

        public async Task<Result<LessonDetailsDto>> Handle(
            UploadLessonAttachmentsCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Attachments == null || !request.Attachments.Any())
                return Result<LessonDetailsDto>.Failure("No attachments provided");

            var lesson = await _lessonRepository.GetAsync(x=> x.Id == request.LessonId);
            if (lesson == null)
                return Result<LessonDetailsDto>.Failure("Lesson not found");

            try
            {
                var paths = await _fileStorage.UploadAttachmentsAsync(request.Attachments);

                var attachments = await AddAttachmentsAsync(lesson.Id, request.Attachments);
                await lessonAttachmentRepository.AddRange(attachments);
                await lessonAttachmentRepository.SaveChangesAsync();

                lesson = await _lessonRepository.GetLessonWithAttachmentsAsync(lesson.Id);
                var dto = mapper.Map<LessonDetailsDto>(lesson);

                return Result<LessonDetailsDto>.Success(dto, "Attachments uploaded successfully");
            }
            catch
            {
                return Result<LessonDetailsDto>.Failure("Failed to upload attachments");
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
