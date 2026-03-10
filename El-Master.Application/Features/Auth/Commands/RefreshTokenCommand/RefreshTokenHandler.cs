using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.RefreshTokenCommand
{
    public class RefreshTokenHandler
    : IRequestHandler<RefreshTokenCommand, Result<AuthModel>>
    {
        private readonly IAuthService _authService;

        public RefreshTokenHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result<AuthModel>> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            if (request.RefreshToken == null)
                return Result<AuthModel>.NotFound("No Tokens Provided"); 

            var result = await _authService.RefreshTokenAsync(request.RefreshToken);

            if (!result.IsAuthenticated)
                return Result<AuthModel>.Failure(result.Message);

            return Result<AuthModel>.Success(result,result.Message);
        }
    }
}
