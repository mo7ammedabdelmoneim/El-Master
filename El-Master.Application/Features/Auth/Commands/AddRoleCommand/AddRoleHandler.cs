using El_Master.Application.Common.Results;
using El_Master.Application.Features.Auth.DTOs;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.AddRoleCommand
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, Result<AddRoleResponseDto>>
    {
        private readonly IAuthService authService;

        public AddRoleHandler(IAuthService authService)
        {
            this.authService = authService;
        }
        public async Task<Result<AddRoleResponseDto>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.AddRoleAsync(request.AddRoleDto);

            if (!string.IsNullOrEmpty(result))
                return Result<AddRoleResponseDto>.Failure(result);

            AddRoleResponseDto addRoleResponse = await authService.GetUserRolesAsyn(request.AddRoleDto.UserId);

            return Result<AddRoleResponseDto>.Success(addRoleResponse, "Role has been added.");

        }
    }
}
