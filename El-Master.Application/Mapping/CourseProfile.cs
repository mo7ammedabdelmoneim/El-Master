using AutoMapper;
using El_Master.Application.Features.Courses.Commands.AddCourseCommand;
using El_Master.Application.Features.Courses.Commands.UpdateCourseCommand;
using El_Master.Application.Features.Courses.Queries.GetAllCoursesQuery;
using El_Master.Application.Features.Grades.Queries.GetAllGradesQuery;
using El_Master.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Mapping
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<AddCourseDto, Course>()
                .ForMember(des => des.Name, options => options.MapFrom(src => src.CourseName))
                .ReverseMap();
            
            CreateMap<UpdateCourseDto, Course>()
                .ReverseMap();
            
            CreateMap<CourseDto, Course>()
                .ReverseMap();

        }
    }
}
