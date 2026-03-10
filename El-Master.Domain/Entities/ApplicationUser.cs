using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace El_Master.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required]
        public Student? Student { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; } = new();

    }
}
