using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Application.Users.Commands.UpdateOneUser
{
    public record UpdateOneUserCommand(string name, string surename, string companyName) : IRequest;

    public class UpdateOneUserCommandHandler : IRequestHandler<UpdateOneUserCommand>
    {
        private readonly IUserService userService;
        public UpdateOneUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public Task<Unit> Handle(UpdateOneUserCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}
