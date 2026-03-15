using AutoMapper;
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
        private readonly IMapper mapper;

        public AddGradeHandler(IGradeRepository gradeRepository, IMapper mapper)
        {
            this.gradeRepository = gradeRepository;
            this.mapper = mapper;
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
            var gradeDto = mapper.Map<GradeDto>(grade);

            return Result<GradeDto>.Success(gradeDto, "Grade has been added successfully.");
        }
    }
}
