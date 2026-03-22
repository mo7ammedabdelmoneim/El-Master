using El_Master.Application.Common.Results;
using El_Master.Application.Features.UserPackages.Queries.GetPackageDetailsQuery;
using MediatR;

namespace El_Master.Application.Features.UserPackages.Queries.GetAllPackagesQuery
{
    public record GetAllPackagesQuery(): IRequest<Result<List<PackageDetailsDto>>>;
}
