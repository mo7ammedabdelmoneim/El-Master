using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Commands.DeleteGradeCommand
{
    public class DeleteGradeHandler
     : IRequestHandler<DeleteGradeCommand, Result<string>>
    {
        private readonly IGradeRepository repository;

        public DeleteGradeHandler(IGradeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<string>> Handle(
            DeleteGradeCommand request,
            CancellationToken cancellationToken)
        {
            var grade = await repository.GetAsync(x=>x.Id == request.Id);
            if (grade == null)
                return Result<string>.Failure("Grade not found");

            repository.Delete(grade);
            await repository.SaveChangesAsync();

            return Result<string>.Success("","Grade deleted successfully");
        }
    }
}
