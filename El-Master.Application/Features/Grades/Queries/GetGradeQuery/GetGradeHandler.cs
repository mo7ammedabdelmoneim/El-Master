using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Queries.GetGradeQuery
{
    public class GetGradeHandler
    : IRequestHandler<GetGradeQuery, Result<GradeDto>>
    {
        private readonly IGradeRepository repository;

        public GetGradeHandler(IGradeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<GradeDto>> Handle(
            GetGradeQuery request,
            CancellationToken cancellationToken)
        {
            var grade = await repository.GetByIdAsync(request.Id);

            if (grade == null)
                return Result<GradeDto>.Failure("Grade not found");

            return Result<GradeDto>.Success(grade,"Grade retrieved successfully.");
        }
    }
}
