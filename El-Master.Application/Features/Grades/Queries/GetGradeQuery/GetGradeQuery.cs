using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Queries.GetGradeQuery
{
    public record GetGradeQuery(Guid Id)
    : IRequest<Result<GradeDto>>;
}
