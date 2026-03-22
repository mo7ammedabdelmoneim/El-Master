using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.CreatePackageCommand
{
    public class CreatePackageHandler
    : IRequestHandler<CreatePackageCommand, Result<PackageDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;

        public CreatePackageHandler(
            ICourseRepository courseRepository,
            IPackageRepository packageRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _packageRepository = packageRepository;
            _mapper = mapper;
        }

        public async Task<Result<PackageDto>> Handle(
            CreatePackageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetAsync(x=>x.Id == request.CourseId);
                if (course == null)
                    return Result<PackageDto>.Failure("Invalid CourseId");

                // Check Duplicate Name
                var exists = await _packageRepository.ExistsAsync(request.CourseId, request.Dto.Name);
                if (exists)
                    return Result<PackageDto>.Failure("Package name already exists for this course");

                var package = _mapper.Map<Package>(request.Dto);
                package.CourseId = request.CourseId;

                await _packageRepository.AddAsync(package);
                await _packageRepository.SaveChangesAsync();

                var resultDto = _mapper.Map<PackageDto>(package);

                return Result<PackageDto>.Success(resultDto, "Package created successfully");
            }
            catch
            {
                return Result<PackageDto>.Failure("Failed to create package");
            }
        }
    }
}
