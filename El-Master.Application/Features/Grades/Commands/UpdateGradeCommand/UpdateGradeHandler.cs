using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Commands.UpdateGradeCommand
{
    public class UpdateGradeHandler
    : IRequestHandler<UpdateGradeCommand, Result<GradeDto>>
    {
        private readonly IGradeRepository repository;

        public UpdateGradeHandler(IGradeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<GradeDto>> Handle(
            UpdateGradeCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Dto.Name))
                return Result<GradeDto>.Failure("Grade Name is required!");

            var grade = await repository.GetAsync(x => x.Id == request.Id);
            if (grade == null)
                return Result<GradeDto>.Failure("Grade not found");
            grade.Name = request.Dto.Name;
            repository.Update(grade);
            await repository.SaveChangesAsync();

            var updatedGrade = new GradeDto { Id = request.Id, Name = request.Dto.Name };

            return Result<GradeDto>.Success(updatedGrade,"Grade updated successfully");
        }
    }
}
