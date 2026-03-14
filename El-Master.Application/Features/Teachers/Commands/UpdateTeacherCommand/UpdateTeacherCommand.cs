using El_Master.Application.Common.Results;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand
{
    public record UpdateTeacherCommand(Guid Id, UpdateTeacherDto Dto) : IRequest<Result<TeacherDto>>;
}
