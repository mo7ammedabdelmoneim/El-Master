using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.Lessons.Queries.GetLessonQuery
{
    public class GetLessonByIdHandler
     : IRequestHandler<GetLessonQuery, Result<LessonDetailsDto>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper mapper;
        public GetLessonByIdHandler(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            this.mapper = mapper;
        }

        public async Task<Result<LessonDetailsDto>> Handle(
            GetLessonQuery request,
            CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetLessonWithAttachmentsAsync(request.Id);

            if (lesson == null)
                return Result<LessonDetailsDto>.Failure("Lesson not found");

            var dto = mapper.Map<LessonDetailsDto>(lesson);

            return Result<LessonDetailsDto>.Success(dto,"Lesson data retrieved successfully.");
        }
    }
}
