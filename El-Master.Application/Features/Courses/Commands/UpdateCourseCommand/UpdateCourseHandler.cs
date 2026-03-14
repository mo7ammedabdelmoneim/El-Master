using El_Master.Application.Common.Results;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.UpdateCourseCommand
{
    public class UpdateCourseHandler
     : IRequestHandler<UpdateCourseCommand, Result<CourseDto>>
    {
        private readonly ICourseRepository repository;

        public UpdateCourseHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<CourseDto>> Handle(
            UpdateCourseCommand request,
            CancellationToken cancellationToken)
        {
            var course = await repository.GetAsync(x=>x.Id == request.Id);

            if (course == null)
                return Result<CourseDto>.Failure("Course not found");

            course.Name = request.Dto.Name;
            course.Description = request.Dto.Description;
            course.GradeId = request.Dto.GradeId;
            course.TeacherId = request.Dto.TeacherId;
            repository.Update(course);
            await repository.SaveChangesAsync();

            var updatedCourse = new CourseDto
            {
                Id = request.Id,
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                GradeId = request.Dto.GradeId,
                TeacherId = request.Dto.TeacherId,
            };

            return Result<CourseDto>.Success(updatedCourse,"Course updated successfully");
        }
    }
}
