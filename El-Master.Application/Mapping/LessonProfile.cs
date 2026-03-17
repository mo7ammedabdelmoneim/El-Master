using AutoMapper;
using El_Master.Application.Features.Lessons.Commands.CreateLessonCommand;
using El_Master.Application.Features.Lessons.Commands.UpdateLesson;
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
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<CreateLessonDto, Lesson>();

            CreateMap<UpdateLessonDto, Lesson>();

            CreateMap<Lesson, LessonDetailsDto>()
                .ForMember(dest => dest.VideoUrl,
                    opt => opt.MapFrom(src => src.VideoPath));

            CreateMap<LessonAttachment, AttachmentDto>()
                .ForMember(dest => dest.FileUrl,
                    opt => opt.MapFrom(src => src.FilePath));
        }
    }
}
