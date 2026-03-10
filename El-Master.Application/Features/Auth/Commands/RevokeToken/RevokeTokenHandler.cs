using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenCommand, Result<string>>
    {
        private readonly IAuthService authService;

        public RevokeTokenHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<Result<string>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.RevokeTokenAsync(request.Token);
            if (!result)
                return Result<string>.Failure("Invalid Token");
            return Result<string>.Success("","Token has been revoked.");
        }
    }
}
