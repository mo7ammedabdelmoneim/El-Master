using El_Master.Application.Common.Results;
using El_Master.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.GetTokenCommand
{
    public record GetTokenCommand(GetTokenDto RequestDto) : IRequest<Result<AuthModel>>;
    
}
