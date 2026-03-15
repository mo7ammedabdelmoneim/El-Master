using AutoMapper;
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
        private readonly IMapper mapper;

        public UpdateCourseHandler(ICourseRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(
            UpdateCourseCommand request,
            CancellationToken cancellationToken)
        {
            var course = await repository.GetAsync(x=>x.Id == request.Id);

            if (course == null)
                return Result<CourseDto>.Failure("Course not found");

            mapper.Map(request.Dto,course);
            repository.Update(course);
            await repository.SaveChangesAsync();

            var updatedCourse = mapper.Map<CourseDto>(course);
            return Result<CourseDto>.Success(updatedCourse,"Course updated successfully");
        }
    }
}
