using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Infrastructure.AppUser.Commands
{
    public record RegisterUserCommand: IRequest<int>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly IAuthorizationManager authorizationManager;

        public RegisterUserCommandHandler(IAuthorizationManager authorizationManager) {
            this.authorizationManager = authorizationManager;
        }

        public Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
