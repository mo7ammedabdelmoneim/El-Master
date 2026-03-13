using El_Master.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.AddTeacherCommand
{
    public record AddTeacherCommand(AddTeacherDto AddTeacherDto): IRequest<Result<AddTeacherDto>>;
}
