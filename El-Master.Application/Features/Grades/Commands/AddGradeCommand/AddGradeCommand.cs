using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using MediatR;

namespace El_Master.Application.Features.Grades.Commands.AddGradeCommand
{
    public record AddGradeCommand(AddGradeDto AddGradeDto):IRequest<Result<GradeDto>>;
}
