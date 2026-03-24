using System.Security.Claims;

namespace El_Master.API.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid? GetStudentId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("studentId")?.Value;
            return !string.IsNullOrEmpty(claim ) ? Guid.Parse(claim) : null;
        }
    }
}
