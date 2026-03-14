using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Commands.UpdateGradeCommand
{
    public record UpdateGradeCommand(Guid Id, UpdateGradeDto Dto) : IRequest<Result<GradeDto>>;
}
