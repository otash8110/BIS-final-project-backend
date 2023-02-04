﻿using FinalProject.Application.Common.Interfaces;
using MediatR;

namespace FinalProject.Application.Users.Commands
{
    public record ApproveUserRegistrationCommand(string id) : IRequest<bool>;

    public class ApproveUserRegistrationHandler : IRequestHandler<ApproveUserRegistrationCommand, bool>
    {
        private readonly IUserService userService;

        public ApproveUserRegistrationHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<bool> Handle(ApproveUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.ApproveUserRegistrationAsync(request.id, cancellationToken);

            return result;
        }
    }
}
