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

        [HttpGet("[action]")]
        public async Task<IActionResult> ApproveUser(string id, CancellationToken token)
        {
            //var result = await mediator.Send(new GetUnregisteredUsersQuery(), token);
            await hubContext.Clients.User(id).SendAsync("SendToUser", "TEST");
            return Ok(true);
        }
    }
}
