using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Lessons.Commands.DeleteAttachmentCommand
{
    public class DeleteAttachmentHandler : IRequestHandler<DeleteAttachmentCommand, Result<string>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileStorageService _fileStorage;

        public DeleteAttachmentHandler(
            ILessonRepository lessonRepository,
            IFileStorageService fileStorage )
        {
            _lessonRepository = lessonRepository;
            _fileStorage = fileStorage;
        }

        public async Task<Result<string>> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var attachment = await _lessonRepository.GetAttachmentByIdAsync(request.AttachmentId);

                if (attachment == null)
                    return Result<string>.Failure("Attachment not found");

                // Delete file from storage
                if (!string.IsNullOrEmpty(attachment.FilePath))
                    _fileStorage.DeleteFile(attachment.FilePath);

                // Remove from DB
                _lessonRepository.RemoveAttachment(attachment);

                await _lessonRepository.SaveChangesAsync();
                return Result<string>.Success("Attachment deleted successfully");
            }
            catch
            {
                return Result<string>.Failure("Failed to delete attachment");
            }
        }
    }
}
