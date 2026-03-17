using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Lessons.Queries.GetCourseLessonsQuery
{
    public class GetCourseLessonsHandler
     : IRequestHandler<GetCourseLessonsQuery, Result<List<LessonDetailsDto>>>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICourseRepository courseRepository;

        public GetCourseLessonsHandler(ILessonRepository lessonRepository, ICourseRepository courseRepository)
        {
            _lessonRepository = lessonRepository;
            this.courseRepository = courseRepository;
        }

        public async Task<Result<List<LessonDetailsDto>>> Handle(
            GetCourseLessonsQuery request,
            CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseByIdAsync(request.CourseId);
            if (course == null)
                return Result<List<LessonDetailsDto>>.Failure("Course not found");

            var lessons = await _lessonRepository.GetLessonsByCourseIdAsync(request.CourseId);

            return Result<List<LessonDetailsDto>>.Success(lessons,"Lessons retrieved successfully.");
        }
    }
}
