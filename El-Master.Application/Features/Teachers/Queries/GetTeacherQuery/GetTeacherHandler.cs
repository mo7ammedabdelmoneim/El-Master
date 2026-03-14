using El_Master.Application.Common.Results;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Queries.GetTeacherQuery
{
    public class GetTeacherHandler
    : IRequestHandler<GetTeacherQuery, Result<TeacherDto>>
    {
        private readonly ITeacherRepository repository;

        public GetTeacherHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<TeacherDto>> Handle(
            GetTeacherQuery request,
            CancellationToken cancellationToken)
        {
            var teacher = await repository.GetTeacherByIdAsync(request.Id);

            if (teacher == null)
                return Result<TeacherDto>.Failure("Teacher not found");

            return Result<TeacherDto>.Success(teacher, "Teacher retrieved successfully.");
        }
    }
}
