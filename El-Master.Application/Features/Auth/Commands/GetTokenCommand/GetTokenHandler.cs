using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Common;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.GetTokenCommand
{
    internal class GetTokenHandler : IRequestHandler<GetTokenCommand, Result<AuthModel>>
    {
        private readonly IAuthService _authService;
        private readonly IValidator<GetTokenDto> validator;

        public GetTokenHandler(IAuthService authService, IValidator<GetTokenDto> validator)
        {
            _authService = authService;
            this.validator = validator;
        }

        public async Task<Result<AuthModel>> Handle(
            GetTokenCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _authService.GetTokenAsync(new GetTokenDto
            {
                Email = request.RequestDto.Email,
                Password = request.RequestDto.Password
            });

            if (!result.IsAuthenticated)
                return Result<AuthModel>.Failure(result.Message);

            return Result<AuthModel>.Success(result,result.Message);
        }
    }
}
