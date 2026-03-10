using El_Master.Application.Common.Results;
using El_Master.Domain.Common;
using MediatR;

namespace El_Master.Application.Features.Auth.Commands.RefreshTokenCommand
{
    public record RefreshTokenCommand(string RefreshToken): IRequest<Result<AuthModel>>;
    
}
