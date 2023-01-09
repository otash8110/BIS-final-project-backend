using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public int UserId { get; set; }
        public bool IsRegistrationApproved { get; set; }
    }
}
