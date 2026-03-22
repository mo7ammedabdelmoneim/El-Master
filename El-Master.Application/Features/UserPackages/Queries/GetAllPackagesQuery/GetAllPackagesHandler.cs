using El_Master.Application.Common.Results;
using El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.UserPackages.Queries.GetAllPackagesQuery
{

    public class GetAllPackagesHandler
        : IRequestHandler<GetAllPackagesQuery, Result<List<PackageDetailsDto>>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetAllPackagesHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Result<List<PackageDetailsDto>>> Handle(
            GetAllPackagesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var packages = await _packageRepository.GetAllPackagesAsync();

                return Result<List<PackageDetailsDto>>.Success(packages, "Packages retrieved successfully");
            }
            catch
            {
                return Result<List<PackageDetailsDto>>.Failure("Failed to retrieve packages");
            }
        }
    }
}
