using FinalProject.API.Policies;
using FinalProject.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinalProject.API.PolicyHandler
{
    public class IsRegisteredRequirementHandler : AuthorizationHandler<IsRegisteredRequirement>
    {
        private readonly IUserService userService;

        public IsRegisteredRequirementHandler(IUserService userService)
        {
            this.userService = userService;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, IsRegisteredRequirement requirement)
        {
            var userEmail = context.User.FindFirstValue(ClaimTypes.Email);

            var isApproved = await userService.IsUserRegistrationApproved(userEmail, CancellationToken.None);

            if (isApproved)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
