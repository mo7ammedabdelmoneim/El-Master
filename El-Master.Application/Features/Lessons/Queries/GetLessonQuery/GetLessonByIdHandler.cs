//using AutoMapper;
//using El_Master.Application.Common.Results;
//using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
//using El_Master.Application.Interfaces.Repositories;
//using MediatR;

//namespace El_Master.Application.Features.Lessons.Queries.GetLessonQuery
//{
//    public class GetLessonByIdHandler
//     : IRequestHandler<GetLessonQuery, Result<LessonDetailsDto>>
//    {
//        private readonly ILessonRepository _lessonRepository;
//        private readonly IMapper mapper;
//        public GetLessonByIdHandler(ILessonRepository lessonRepository, IMapper mapper)
//        {
//            _lessonRepository = lessonRepository;
//            this.mapper = mapper;
//        }

//        public async Task<Result<LessonDetailsDto>> Handle(
//            GetLessonQuery request,
//            CancellationToken cancellationToken)
//        {
//            var lesson = await _lessonRepository.GetLessonWithAttachmentsAsync(request.Id);

//            if (lesson == null)
//                return Result<LessonDetailsDto>.Failure("Lesson not found");

//            var dto = mapper.Map<LessonDetailsDto>(lesson);

//            return Result<LessonDetailsDto>.Success(dto,"Lesson data retrieved successfully.");
//        }
//    }
//}


//using AutoMapper;
//using El_Master.Application.Common.Results;
//using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
//using El_Master.Application.Interfaces.Repositories;
//using MediatR;

//namespace El_Master.Application.Features.Lessons.Queries.GetLessonQuery
//{
//    public class GetLessonByIdHandler
//     : IRequestHandler<GetLessonQuery, Result<LessonDetailsDto>>
//    {
//        private readonly ILessonRepository _lessonRepository;
//        private readonly IMapper mapper;
//        public GetLessonByIdHandler(ILessonRepository lessonRepository, IMapper mapper)
//        {
//            _lessonRepository = lessonRepository;
//            this.mapper = mapper;
//        }

//        public async Task<Result<LessonDetailsDto>> Handle(
//            GetLessonQuery request,
//            CancellationToken cancellationToken)
//        {
//            var lesson = await _lessonRepository.GetLessonWithAttachmentsAsync(request.Id);

//            if (lesson == null)
//                return Result<LessonDetailsDto>.Failure("Lesson not found");

//            var dto = mapper.Map<LessonDetailsDto>(lesson);

//            return Result<LessonDetailsDto>.Success(dto,"Lesson data retrieved successfully.");
//        }
//    }
//}


using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace El_Master.Application.Features.Lessons.Queries.GetLessonQuery
{
    public class GetLessonByIdHandler
        : IRequestHandler<GetLessonQuery, Result<LessonDetailsDto>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetLessonByIdHandler(
            ILessonRepository lessonRepository,
            ISubscriptionRepository subscriptionRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _subscriptionRepository = subscriptionRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<LessonDetailsDto>> Handle(
            GetLessonQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                // current user
                var user = _httpContextAccessor.HttpContext?.User;

                var studentIdClaim = user.FindFirst("studentId")?.Value;

                var studentId = Guid.Parse(studentIdClaim);

                // Check access
                var hasAccess = await _subscriptionRepository
                    .HasAccessToLessonAsync(studentId, request.Id);

                if (!hasAccess)
                    return Result<LessonDetailsDto>
                        .Failure("You are not subscribed to this lesson");

                // Get lesson
                var lesson = await _lessonRepository
                    .GetLessonWithAttachmentsAsync(request.Id);

                if (lesson == null)
                    return Result<LessonDetailsDto>.Failure("Lesson not found");

                var dto = _mapper.Map<LessonDetailsDto>(lesson);


                return Result<LessonDetailsDto>
                    .Success(dto, "Lesson data retrieved successfully.");
            }
            catch
            {
                return Result<LessonDetailsDto>
                    .Failure("Failed to retrieve lesson");
            }
        }
    }
}