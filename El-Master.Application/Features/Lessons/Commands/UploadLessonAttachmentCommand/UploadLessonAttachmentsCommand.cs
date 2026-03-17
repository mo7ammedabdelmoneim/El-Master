using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Lessons.Commands.UploadLessonAttachmentCommand
{
    public record UploadLessonAttachmentsCommand(Guid LessonId, List<IFormFile> Attachments)
    : IRequest<Result<LessonDetailsDto>>;
}
