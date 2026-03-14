using El_Master.Application.Common.Results;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand
{
    public class UpdateTeacherHandler
     : IRequestHandler<UpdateTeacherCommand, Result<TeacherDto>>
    {
        private readonly ITeacherRepository repository;

        public UpdateTeacherHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<TeacherDto>> Handle(
            UpdateTeacherCommand request,
            CancellationToken cancellationToken)
        {
            var teacher = await repository.GetAsync(x=> x.Id == request.Id);
            if (teacher == null)
                return Result<TeacherDto>.Failure("Teacher not found");
            teacher.FirstName = request.Dto.FirstName;
            teacher.LastName = request.Dto.LastName;
            teacher.Bio = request.Dto.Bio;
            teacher.ImageUrl = request.Dto.ImageUrl;

            repository.Update(teacher);
            await repository.SaveChangesAsync();

            var updatedTeacher = await repository.GetTeacherByIdAsync(request.Id);

            return Result<TeacherDto>.Success(updatedTeacher,"Teacher updated successfully");
        }
    }
}
