using AutoMapper;
using El_Master.Application.Features.Teachers.Commands.AddTeacherCommand;
using El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand;
using El_Master.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Mapping
{
    public class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            CreateMap<AddTeacherDto, ApplicationUser>();
            CreateMap<AddTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();
        }
    }
}
