using FinalProject.Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUser(string email, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetUserQuery(email), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser()
        {
            return Ok();
        }
    }
}
