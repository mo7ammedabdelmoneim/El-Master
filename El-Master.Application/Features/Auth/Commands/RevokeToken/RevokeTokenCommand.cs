using El_Master.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.RevokeToken
{
    public record RevokeTokenCommand(string Token) : IRequest<Result<string>>;
}
