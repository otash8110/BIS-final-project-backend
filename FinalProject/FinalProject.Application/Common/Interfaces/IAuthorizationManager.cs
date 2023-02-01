using FinalProject.Application.Common.Results;
using FinalProject.Core.Enums;

namespace FinalProject.Application.Common.Interfaces
{
    public interface IAuthorizationManager
    {
        Task<bool> CreateUserAsync(string email,
            string name,
            string surname,
            string password,
            Roles role);

        Task<LoginResult> LoginAsync(string email, string password);
    }
}
