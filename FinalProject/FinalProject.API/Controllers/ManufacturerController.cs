using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Offers.Queries.GetManufacturerOffers;
using FinalProject.Application.Products.Queries.GetOneProductWithManufacturer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Manufacturer,Admin", Policy = "IsRegistrationApproved")]
    public class ManufacturerController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public ManufacturerController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Offers(CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetManufacturerOffersQuery(), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
