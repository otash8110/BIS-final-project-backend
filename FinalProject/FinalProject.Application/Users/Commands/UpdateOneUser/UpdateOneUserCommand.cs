using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Application.Users.Commands.UpdateOneUser
{
    public class UpdateOneUserCommand : IRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
    }

    public class UpdateOneUserCommandHandler : IRequestHandler<UpdateOneUserCommand>
    {
        private readonly IUserService userService;
        public UpdateOneUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Unit> Handle(UpdateOneUserCommand request, CancellationToken cancellationToken)
        {
            await userService.UpdateUser(request.Email, request.Name, request.Surname, request.CompanyName, cancellationToken);

            return Unit.Value;
        }
    }
}
