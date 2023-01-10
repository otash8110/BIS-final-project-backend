using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infrastructure.Identity
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AuthorizationManager(UserManager<ApplicationUser> userManager) { 
            this.userManager = userManager;
        }

        public async Task<bool> CreateUserAsync(string email,
            string password,
            Roles role,
            int userId)
        {
            var appUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
                UserId= userId,
                IsRegistrationApproved = false
            };

            var savedAppUser = await userManager.CreateAsync(appUser, password);
            var roles = new string[]
            {
                Roles.User.ToString(),
            };

            if (role != Roles.User) roles.Append(role.ToString());

            await userManager.AddToRolesAsync(appUser, roles);

            return savedAppUser.Succeeded;
        }
    }
}
