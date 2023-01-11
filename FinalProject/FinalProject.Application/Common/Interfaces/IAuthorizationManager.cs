using FinalProject.Core.Enums;

namespace FinalProject.Application.Common.Interfaces
{
    public interface IAuthorizationManager
    {
        Task<bool> CreateUserAsync(string email, string password, Roles role, int userId);

        Task<string> LoginAsync(string email, string password);
    }
}
