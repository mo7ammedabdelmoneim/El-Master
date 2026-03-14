using El_Master.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.DeleteTeacherCommand
{
    public record DeleteTeacherCommand(Guid Id) : IRequest<Result<string>>;
}
