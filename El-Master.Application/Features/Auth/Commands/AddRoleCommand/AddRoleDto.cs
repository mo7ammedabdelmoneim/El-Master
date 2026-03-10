using System.ComponentModel.DataAnnotations;

namespace El_Master.Application.Features.Auth.Commands.AddRoleCommand
{
    public class AddRoleDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
