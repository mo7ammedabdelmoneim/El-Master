using AutoMapper;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Domain.Entities;

namespace El_Master.Application.Mapping
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeDto>();
        }
    }
}
