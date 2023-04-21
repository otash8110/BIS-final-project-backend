using FinalProject.API.Services;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Products.Queries.GetProducts;
using FinalProject.Application.Search.Queries.GetSearchProducts;
using FinalProject.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Distributor,Admin", Policy = "IsRegistrationApproved")]
    public class SearchController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public SearchController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Products(CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetSearchProductsQuery(), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
