using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery
{
    public class GetAllTeachersHandler
    : IRequestHandler<GetAllTeachersQuery, Result<IEnumerable<TeacherDto>>>
    {
        private readonly ITeacherRepository teacherRepository;

        public GetAllTeachersHandler(ITeacherRepository teacherRepository)
        {
            this.teacherRepository = teacherRepository;
        }

        public async Task<Result<IEnumerable<TeacherDto>>> Handle(
            GetAllTeachersQuery request,
            CancellationToken cancellationToken)
        {
            var teachers = await teacherRepository.GetAllTeachersAsync();

            return Result<IEnumerable<TeacherDto>>
                .Success(teachers, "Teachers retrieved successfully");
        }
    }
}
