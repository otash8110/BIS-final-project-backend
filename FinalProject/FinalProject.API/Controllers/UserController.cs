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
        public IActionResult GetUser(string email)
        {
            return View();
        }
    }
}
