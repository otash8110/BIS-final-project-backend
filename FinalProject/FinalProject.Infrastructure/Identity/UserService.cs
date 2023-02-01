using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Common.Results;
using FinalProject.Core.Enums;
using FinalProject.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infrastructure.Identity
{
    public class UserService : IUserSercive
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;

        public UserService(UserManager<ApplicationUser> userManager,
            ITokenService tokenService) { 
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<bool> CreateUserAsync(string email,
            string name,
            string surname,
            string password,
            Roles role)
        {
            var appUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
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

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            try
            {
                var appUser = await userManager.FindByEmailAsync(email);


                if (appUser != null & await userManager.CheckPasswordAsync(appUser, password))
                {
                    var userRoles = await userManager.GetRolesAsync(appUser);
                    var token = tokenService.CreateUserToken(appUser, userRoles);
                    
                    return new LoginResult()
                    {
                        AccessToken= token,
                        IsRegistrationApproved = appUser.IsRegistrationApproved,
                    };
                }
                else
                {
                    throw new UnauthorizedAccessException("User not found or password is not correct");
                }
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("User not found or password is not correct");
            }
        }
    }
}
