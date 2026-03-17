using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Common;
using El_Master.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Teachers.Commands.AddTeacherCommand
{
    public class AddTeacherHandler : IRequestHandler<AddTeacherCommand, Result<AddTeacherDto>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITeacherRepository teacherRepository;
        private readonly IImageService imageService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddTeacherHandler(
            UserManager<ApplicationUser> userManager, ITeacherRepository teacherRepository, IImageService imageService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.teacherRepository = teacherRepository;
            this.imageService = imageService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<AddTeacherDto>> Handle(
            AddTeacherCommand request,
            CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync();

            try
            {
                var dto = request.AddTeacherDto;

                var existingUser = await userManager.FindByEmailAsync(dto.Email);

                if (existingUser != null)
                    return Result<AddTeacherDto>.Failure("Email already exists");

                var user = mapper.Map<ApplicationUser>(dto);
                user.UserName = dto.Email;

                var result = await userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                    return Result<AddTeacherDto>.Failure("Failed to create user");

                await userManager.AddToRoleAsync(user, "Teacher");

                string imageUrl = null;

                if (dto.Image != null)
                    imageUrl = await imageService.UploadImageAsync(dto.Image);

                var teacher = mapper.Map<Teacher>(dto);
                teacher.ApplicationUserId = user.Id;
                  

                await teacherRepository.AddAsync(teacher);
                await teacherRepository.SaveChangesAsync();

                await unitOfWork.CommitAsync();

                return Result<AddTeacherDto>.Success(dto, "Teacher created successfully");
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                return Result<AddTeacherDto>.Failure($"Failed to create teacher: {ex.Message}");
            }
        }
    }
}