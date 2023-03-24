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

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRegisteredRequirement requirement)
        {
            var userId = context.User.FindFirstValue("UserId");

            userService.GetUser
        }
    }
}
