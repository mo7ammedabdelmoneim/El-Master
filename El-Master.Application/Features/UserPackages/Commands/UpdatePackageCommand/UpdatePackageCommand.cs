using El_Master.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.UserPackages.Commands.UpdatePackageCommand
{
    public record UpdatePackageCommand(Guid PackageId, UpdatePackageDto Dto) : IRequest<Result<bool>>;
}
