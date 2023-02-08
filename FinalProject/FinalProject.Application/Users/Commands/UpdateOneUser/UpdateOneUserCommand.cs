using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Application.Users.Commands.UpdateOneUser
{
    public record UpdateOneUserCommand(string email, string name, string surename, string companyName) : IRequest;

    public class UpdateOneUserCommandHandler : IRequestHandler<UpdateOneUserCommand>
    {
        private readonly IUserService userService;
        public UpdateOneUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Unit> Handle(UpdateOneUserCommand request, CancellationToken cancellationToken)
        {
            await userService.UpdateUser(request.email, request.name, request.surename, request.companyName, cancellationToken);

            return Unit.Value;
        }
    }
}
