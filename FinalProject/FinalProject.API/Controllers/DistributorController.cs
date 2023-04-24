using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Products.Queries.GetOneProductWithManufacturer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Distributor,Admin", Policy = "IsRegistrationApproved")]
    public class DistributorController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public DistributorController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct([FromQuery] int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetOneProductWithManufacturerQuery(id), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
