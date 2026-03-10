using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Features.Auth.DTOs
{
    public class AddRoleResponseDto
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

    }
}
