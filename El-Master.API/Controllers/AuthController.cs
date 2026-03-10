using Azure;
using El_Master.API.Extensions;
using El_Master.API.Responses;
using El_Master.Application.Features.Auth.Commands.AddRoleCommand;
using El_Master.Application.Features.Auth.Commands.GetTokenCommand;
using El_Master.Application.Features.Auth.Commands.RefreshTokenCommand;
using El_Master.Application.Features.Auth.Commands.RegisterCommand;
using El_Master.Application.Features.Auth.Commands.RevokeToken;
using El_Master.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace El_Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var command = new RegisterCommand(dto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                SetRefreshTokenInCookie(result.Value.RefreshToken,
                                    result.Value.RefreshTokenExpiration);

            return result.ToApiResponse();
        }

        [HttpPost("get-token")]
        public async Task<IActionResult> GetTokenAsync(GetTokenDto token)
        {
            var command = new GetTokenCommand(token); 
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return result.ToApiResponse();

            if (!string.IsNullOrEmpty(result.Value.RefreshToken))
            {
                SetRefreshTokenInCookie(result.Value.RefreshToken,
                                        result.Value.RefreshTokenExpiration);
            }

            return result.ToApiResponse();
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRoleAsync(AddRoleDto dto)
        {
            var command = new AddRoleCommand(dto);
            var result = await _mediator.Send(command);

            return result.ToApiResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken)
            {
                RefreshToken = refreshToken
            });

            if (!result.IsSuccess)
                return result.ToApiResponse();

            SetRefreshTokenInCookie(result.Value.RefreshToken,
                                    result.Value.RefreshTokenExpiration);

            return result.ToApiResponse();
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return result.ToApiResponse();
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
