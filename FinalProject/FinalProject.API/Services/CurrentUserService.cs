using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Enums;
using System.Security.Claims;

namespace FinalProject.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");

        public string UserEmail => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public Roles Role => Enum.Parse<Roles>(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role));
    }
}
