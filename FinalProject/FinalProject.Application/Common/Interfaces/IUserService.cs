﻿using FinalProject.Application.Common.Results;
using FinalProject.Application.Users.Queries;
using FinalProject.Application.Users.Queries.GetUser;
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

        Task<bool> ApproveUserRegistrationAsync(string email, CancellationToken token);

        Task<UserDTO> GetUser(string email, CancellationToken token);

        Task UpdateUser(string email, string name, string surname, string companyName, CancellationToken token);

        Task<bool> IsUserRegistrationApproved(string email, CancellationToken token);
    }
}
