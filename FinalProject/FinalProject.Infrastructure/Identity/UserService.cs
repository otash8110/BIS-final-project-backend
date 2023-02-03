using AutoMapper;
using AutoMapper.Execution;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Common.Results;
using FinalProject.Application.Users.Queries;
using FinalProject.Core.Enums;
using FinalProject.Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public UserService(UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IMapper mapper) { 
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
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
                Name = name,
                Surname = surname,
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

        public async Task<List<NotRegisteredUserResult>> GetUnregisteredUsers(CancellationToken cancellationToken)
        {
            var result = await userManager.Users.Where(user => user.IsRegistrationApproved != true)
                .ToListAsync();
            
            // NEVER USE SUCH CODE; IT IS BAAAAAAD
            result.ForEach(item =>
            {
                item.Role = userManager.GetRolesAsync(item).Result;
            });
            var users = mapper.Map<List<NotRegisteredUserResult>>(result);
            return users;
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
