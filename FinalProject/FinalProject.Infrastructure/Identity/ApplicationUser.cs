using FinalProject.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public bool IsRegistrationApproved { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
    }
}
