using El_Master.Application.Common.Results;
using El_Master.Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.Commands.AddRoleCommand
{
    public record AddRoleCommand(AddRoleDto AddRoleDto):IRequest<Result<AddRoleResponseDto>>;
}