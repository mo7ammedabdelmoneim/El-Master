using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace El_Master.Application.Features.Lessons.Commands.CreateLessonCommand
{
    public class CreateLessonHandler : IRequestHandler<CreateLessonCommand, Result<LessonDetailsDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonHandler(
            ICourseRepository courseRepository,
            ILessonRepository lessonRepository,
            IFileStorageService fileStorage,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _lessonRepository = lessonRepository;
            _fileStorage = fileStorage;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LessonDetailsDto>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            string? videoPath = null;

            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
                if (course == null)
                    return Result<LessonDetailsDto>.Failure("Invalid CourseId");

                var orderExists = await _lessonRepository.GetAsync(x => x.CourseId == request.CourseId && x.Order == request.Dto.Order);

                if (orderExists != null)
                    return Result<LessonDetailsDto>.Failure("Lesson order already exists in this course");

                videoPath = await _fileStorage.UploadVideoAsync(request.Dto.Video);

                var lesson = _mapper.Map<Lesson>(request.Dto);
                lesson.CourseId = request.CourseId;
                lesson.VideoPath = videoPath;

                await _lessonRepository.AddAsync(lesson);

                await _lessonRepository.SaveChangesAsync();

                var lessonDto = _mapper.Map<LessonDetailsDto>(lesson);

                return Result<LessonDetailsDto>.Success(lessonDto,"Lesson Created Successfully.");
            }
            catch
            {
                if (videoPath != null)
                    _fileStorage.DeleteFile(videoPath);

                return Result<LessonDetailsDto>.Failure("Failed to create lesson");
            }
        }

            private async Task AddAttachmentsAsync(Lesson lesson, List<IFormFile> attachments)
        {
            var paths = await _fileStorage.UploadAttachmentsAsync(attachments);

            foreach (var (file, path) in attachments.Zip(paths))
            {
                lesson.Attachments.Add(new LessonAttachment
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    FilePath = path,
                    FileType = Path.GetExtension(file.FileName).TrimStart('.'),
                    FileSize = file.Length,
                    LessonId = lesson.Id
                });
            }
        }
    }
}