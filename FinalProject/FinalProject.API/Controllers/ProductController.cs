using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Products.Commands.CreateOneProduct;
using FinalProject.Application.Products.Commands.UpdateOneProduct;
using FinalProject.Application.Products.Queries.GetOneProduct;
using FinalProject.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            try
            {
                var user = currentUserService.UserId;
                var result = await mediator.Send(new GetProductsQuery(user), cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetOneProductQuery(id), cancellationToken);
                return Ok(result);
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

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateOneProductCommand cmd, CancellationToken cancellationToken)
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
    }
}
