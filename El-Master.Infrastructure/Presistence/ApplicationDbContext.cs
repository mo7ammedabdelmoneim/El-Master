using El_Master.Domain.Entities;
using El_Master.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace El_Master.Infrastructure.Presistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public  DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public  DbSet<Grade> Grades { get; set; }
    }
}
