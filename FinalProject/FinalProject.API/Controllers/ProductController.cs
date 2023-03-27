using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Products.Commands.CreateOneProduct;
using FinalProject.Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Manufacturer,Admin", Policy = "IsRegistrationApproved")]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserService currentUserService;

        public ProductController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this.mediator = mediator;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateOneProductCommand cmd, CancellationToken cancellationToken)
        {
            try
            {
                cmd.UserId = currentUserService.UserId;
                var result = await mediator.Send(cmd, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
