using El_Master.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.AddCourseCommand
{
    public record AddCourseCommand(AddCourseDto AddCourseDto) : IRequest<Result<AddCourseDto>>
    {
    }
}
