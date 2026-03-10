using El_Master.Application.Features.Auth.Commands.AddRoleCommand;
using El_Master.Application.Features.Auth.Commands.GetTokenCommand;
using El_Master.Application.Features.Auth.Commands.RegisterCommand;
using El_Master.Application.Features.Auth.Commands.RevokeToken;
using El_Master.Application.Features.Auth.DTOs;
using El_Master.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);
        Task<AuthModel> GetTokenAsync(GetTokenDto model);
        Task<string> AddRoleAsync(AddRoleDto model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(RevokeTokenDto revokeToken);
        Task<AddRoleResponseDto> GetUserRolesAsyn(string userId);
    }
}
