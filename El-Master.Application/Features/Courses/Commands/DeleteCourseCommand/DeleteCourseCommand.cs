using El_Master.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.DeleteCourseCommand
{
    public record DeleteCourseCommand(Guid Id) : IRequest<Result<string>>;
}
