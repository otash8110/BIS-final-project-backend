using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Offers.Commands.CreateOneOffer;
using FinalProject.Application.Offers.Queries.GetOneOfferById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Distributor,Admin", Policy = "IsRegistrationApproved")]
    public class OfferController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public OfferController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer(CreateOneOfferCommand cmd, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(cmd, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffer(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetOneOfferByIdQuery(id), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
