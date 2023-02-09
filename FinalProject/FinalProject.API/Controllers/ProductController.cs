using FinalProject.Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Manufacturer")]
    public class ProductController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CancellationToken cancellationToken)
        {
            //try
            //{
            //    var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //    var result = await mediator.Send(new GetUserQuery(userEmail), cancellationToken);
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

            return Ok();
        }
    }
}
