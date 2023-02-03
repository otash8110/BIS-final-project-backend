using AutoMapper;
using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Application.Users.Queries
{
    public record GetUnregisteredUsersQuery : IRequest<UnregisteredUserResult>;

    public class GetUnregisteredUsersHandler : IRequestHandler<GetUnregisteredUsersQuery, UnregisteredUserResult>
    {
        private readonly IUserService userService;

        public GetUnregisteredUsersHandler(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
        }

        public async Task<UnregisteredUserResult> Handle(GetUnregisteredUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await Task.Run(() => userService.GetUnregisteredUsers());
            var result = new UnregisteredUserResult()
            {
                Data = users
            };
            return result;
        }
    }
}
