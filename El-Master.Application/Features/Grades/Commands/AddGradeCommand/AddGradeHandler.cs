using El_Master.Application.Common.Results;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Grades.Commands.AddGradeCommand
{
    public class AddGradeHandler : IRequestHandler<AddGradeCommand, Result<GradeDto>>
    {
        private readonly IGradeRepository gradeRepository;

        public AddGradeHandler(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }
        public async Task<Result<GradeDto>> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.AddGradeDto.Name ))
                return Result<GradeDto>.Failure("Grade Name is required!");

            var grade = new Grade
            {
                Name = request.AddGradeDto.Name
            };

            await gradeRepository.AddAsync(grade);
            await gradeRepository.SaveChangesAsync();

            return Result<GradeDto>.Success(new GradeDto { Id= grade.Id, Name= grade.Name }, "Grade has been added successfully.");
        }
    }
}
