using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public class AddCourseHandler : IRequestHandler<AddCourseCommand, Result<AddCourseDto>>
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IGradeRepository gradeRepository;
        private readonly ICourseRepository courseRepository;

        public AddCourseHandler(ITeacherRepository teacherRepository, IGradeRepository gradeRepository, ICourseRepository courseRepository)
        {
            this.teacherRepository = teacherRepository;
            this.gradeRepository = gradeRepository;
            this.courseRepository = courseRepository;
        }
        public async Task<Result<AddCourseDto>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            // check teacher
            var teacher = await teacherRepository.GetByIdAsync(request.AddCourseDto.TeacherId);
            if (teacher == null)
                return Result<AddCourseDto>.Failure("Invalid TeacherId");

            // check grade
            var grade = await gradeRepository.GetByIdAsync(request.AddCourseDto.GradeId);
            if (grade == null)
                return Result<AddCourseDto>.Failure("Invalid GradeId");

            var course = new Course
            {
                Name = request.AddCourseDto.CourseName,
                Description = request.AddCourseDto.Description,
                TeacherId = request.AddCourseDto.TeacherId,
                GradeId = request.AddCourseDto.GradeId
            };

            await courseRepository.AddAsync(course);
            await courseRepository.SaveChangesAsync();

            return Result<AddCourseDto>.Success(request.AddCourseDto, "Course has been added successfully.");
        }
    }
}
