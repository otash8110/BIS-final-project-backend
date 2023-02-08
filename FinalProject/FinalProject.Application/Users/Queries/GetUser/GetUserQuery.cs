using FinalProject.Application.Common.Interfaces;
using MediatR;
using System;

namespace FinalProject.Application.Users.Queries.GetUser
{
    public record GetUserQuery(string email) : IRequest<UserDTO>;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {
        private readonly IUserService userService;

        public GetUserQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await userService.GetUser(request.email, cancellationToken);
        }
    }
}
