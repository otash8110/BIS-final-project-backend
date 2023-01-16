using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Infrastructure.AppUser.Queries
{
    public record LoginUserQuery: IRequest<string>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IAuthorizationManager authorizationManager;

        public LoginUserQueryHandler(IAuthorizationManager authorizationManager)
        {
            this.authorizationManager = authorizationManager;
        }

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var userToken = await authorizationManager.LoginAsync(request.Email, request.Password);

            return userToken;
        }
    }
}
