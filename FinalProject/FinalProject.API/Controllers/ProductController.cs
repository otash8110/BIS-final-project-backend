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

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateOneProductCommand cmd, CancellationToken cancellationToken)
        {
            try
            {
                var userId = User.FindFirstValue("UserId");
                cmd.UserId = userId;
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
