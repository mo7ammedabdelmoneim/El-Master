using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.DeleteTeacherCommand
{
    public class DeleteTeacherHandler
     : IRequestHandler<DeleteTeacherCommand, Result<string>>
    {
        private readonly ITeacherRepository repository;

        public DeleteTeacherHandler(ITeacherRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<string>> Handle(
            DeleteTeacherCommand request,
            CancellationToken cancellationToken)
        {
            var teacher = await repository.GetAsync(x=> x.Id == request.Id);

            if (teacher == null)
                return Result<string>.Failure("Teacher not found");

            return Result<string>.Success("","Teacher deleted successfully");
        }
    }
}
