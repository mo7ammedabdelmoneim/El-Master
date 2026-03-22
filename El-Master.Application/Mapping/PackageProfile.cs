using AutoMapper;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Features.Lessons.Commands.UpdateLesson;
using El_Master.Application.Features.Teachers.Commands.AddTeacherCommand;
using El_Master.Application.Features.Teachers.Commands.UpdateTeacherCommand;
using El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand;
using El_Master.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Mapping
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMap<Package,PackageDto>();
            CreateMap<CreatePackageDto,Package>();
        }
    }
}
