using FinalProject.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator)
        { 
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> UnregisteredUsers()
        {
            var result = await mediator.Send(new GetUnregisteredUsersQuery());
            return Ok(result);
        } 

    }
}
