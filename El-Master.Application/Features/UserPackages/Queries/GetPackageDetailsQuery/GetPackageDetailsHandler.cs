using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery
{

    public class GetPackageDetailsHandler
        : IRequestHandler<GetPackageDetailsQuery, Result<PackageDetailsDto>>
    {
        private readonly IPackageRepository _packageRepository;

        public GetPackageDetailsHandler(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public async Task<Result<PackageDetailsDto>> Handle(
            GetPackageDetailsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                // 1️⃣ Get package
                var package = await _packageRepository.GetPackageDetailsAsync(request.PackageId);

                if (package == null)
                    return Result<PackageDetailsDto>.Failure("Package not found");

                return Result<PackageDetailsDto>.Success(package, "Package retrieved successfully");
            }
            catch
            {
                return Result<PackageDetailsDto>.Failure("Failed to retrieve package");
            }
        }
    }
}
