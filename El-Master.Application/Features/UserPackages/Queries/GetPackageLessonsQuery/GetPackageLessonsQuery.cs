using El_Master.Application.Common.Results;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Queries.GetPackageLessonsQuery
{
    public record GetPackageLessonsQuery(Guid PackageId): IRequest<Result<List<LessonDetailsDto>>>;
}
