using AutoMapper;
using AutoMapper.Execution;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Common.Results;
using FinalProject.Application.Users.Queries;
using FinalProject.Application.Users.Queries.GetUser;
using FinalProject.Core.Enums;
using FinalProject.Core.Exceptions;
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

        public async Task<bool> ApproveUserRegistrationAsync(string email, CancellationToken token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) throw new UserNotFoundException(email);

            user.IsRegistrationApproved = true;
            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
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

        public async Task<List<NotRegisteredUserResult>> GetUnregisteredUsersAsync(CancellationToken cancellationToken)
        {
            var result = await userManager.Users.Where(user => user.IsRegistrationApproved != true)
                .ToListAsync();

            var asyncResult = result.Select(async item => new ApplicationUser()
            {
                Id = item.Id,
                IsRegistrationApproved = item.IsRegistrationApproved,
                Name = item.Name,
                Surname = item.Surname,
                CompanyName = item.CompanyName,
                Email = item.Email,
                Role = await userManager.GetRolesAsync(item)
            });

            var mainResult = asyncResult.Select(i => i.Result);


            var users = mapper.Map<List<NotRegisteredUserResult>>(mainResult);
            return users;
        }

        public async Task<UserDTO> GetUser(string email, CancellationToken token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) throw new UserNotFoundException(email);

            return mapper.Map<UserDTO>(user);
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
