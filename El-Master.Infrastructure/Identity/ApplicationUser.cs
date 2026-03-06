using El_Master.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace El_Master.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }
        [Required]
        public Guid GradeId { get; set; }

        [ForeignKey("GradeId")]
        public Grade Grade { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; } = new();

    }
}
