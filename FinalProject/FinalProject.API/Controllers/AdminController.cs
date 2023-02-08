using FinalProject.Application.Users.Commands;
using FinalProject.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdminController : Controller
    {
        private readonly IMediator mediator;
        private readonly IHubContext<SignalHub> hubContext;
        private readonly IConfiguration configuration;


        public AdminController(IMediator mediator,
            IHubContext<SignalHub> hubContext
,
            IConfiguration configuration)
        {
            this.mediator = mediator;
            this.hubContext = hubContext;
            this.configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUnregisteredUsers(CancellationToken token)
        {
            var result = await mediator.Send(new GetUnregisteredUsersQuery(), token);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ApproveUser(ApproveUserRegistrationCommand cmd, CancellationToken token)
        {
            var result = await mediator.Send(cmd, token);
            await hubContext.Clients.User(cmd.email).SendAsync("SendToUser", "Your registration was approved");
            return Ok(result);
        }
    }
}
