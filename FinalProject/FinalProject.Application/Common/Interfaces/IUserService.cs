using FinalProject.Application.Common.Results;
using FinalProject.Application.Users.Queries;
using FinalProject.Core.Enums;

namespace FinalProject.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(string email,
            string name,
            string surname,
            string password,
            Roles role);

        Task<LoginResult> LoginAsync(string email, string password);

        Task<List<NotRegisteredUserResult>> GetUnregisteredUsersAsync(CancellationToken token);

        Task<bool> ApproveUserRegistrationAsync(string id, CancellationToken token);
    }
}
