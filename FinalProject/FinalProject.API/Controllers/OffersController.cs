using FinalProject.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Distributor,Admin", Policy = "IsRegistrationApproved")]
    public class OffersController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public OffersController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        public IActionResult CreateOffer()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult GetOffer()
        {
            return View();
        }
    }
}
