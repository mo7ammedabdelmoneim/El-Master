using El_Master.Application.Common.Results;
using El_Master.Application.Features.Teachers.Queries.GetAllTeachersQuery;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Queries.GetAllGradesQuery
{
    public record GetAllGradesQuery(): IRequest<Result<IEnumerable<GradeDto>>>;
}
