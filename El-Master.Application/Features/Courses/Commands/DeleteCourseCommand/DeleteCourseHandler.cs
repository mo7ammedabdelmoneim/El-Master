using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Courses.Commands.DeleteCourseCommand
{
    public class DeleteCourseHandler
    : IRequestHandler<DeleteCourseCommand, Result<string>>
    {
        private readonly ICourseRepository repository;

        public DeleteCourseHandler(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<string>> Handle(
            DeleteCourseCommand request,
            CancellationToken cancellationToken)
        {
            var course = await repository.GetAsync(x=> x.Id == request.Id);

            if (course == null)
                return Result<string>.Failure("Course not found");
            repository.Delete(course);
            await repository.SaveChangesAsync();

            return Result<string>.Success("", "Course deleted successfully");
        }
    }
}
