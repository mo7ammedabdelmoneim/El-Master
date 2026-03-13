using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Queries.GetAllGradesQuery
{
    public class GetAllGradesHandler: IRequestHandler<GetAllGradesQuery, Result<IEnumerable<GradeDto>>>
    {
        private readonly IGradeRepository gradeRepository;

        public GetAllGradesHandler(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public async Task<Result<IEnumerable<GradeDto>>> Handle(
            GetAllGradesQuery request,
            CancellationToken cancellationToken)
        {
            var grades = await gradeRepository.GetAllGradesAsync();

            return Result<IEnumerable<GradeDto>>
                .Success(grades,"Grades Retrived Successfully.");
        }
    }
}
