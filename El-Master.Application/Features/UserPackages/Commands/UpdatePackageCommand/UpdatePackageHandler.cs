using AutoMapper;
using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.UserPackages.Commands.UpdatePackageCommand
{

    public class UpdatePackageHandler
        : IRequestHandler<UpdatePackageCommand, Result<bool>>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper mapper;

        public UpdatePackageHandler(
            IPackageRepository packageRepository,IMapper mapper)
        {
            _packageRepository = packageRepository;
            this.mapper = mapper;
        }

        public async Task<Result<bool>> Handle(
            UpdatePackageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var package = await _packageRepository.GetAsync(x=>x.Id == request.PackageId);

                if (package == null)
                    return Result<bool>.Failure("Package not found");

                mapper.Map(request.Dto, package);

                await _packageRepository.SaveChangesAsync();

                return Result<bool>.Success(true, "Package updated successfully");
            }
            catch
            {
                return Result<bool>.Failure("Failed to update package");
            }
        }
    }
}
