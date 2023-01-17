using FinalProject.Application.Common.Interfaces;
using FinalProject.Infrastructure.AppUser.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthorizationManager authorizationManager;
        private readonly IHubContext<SignalHub> hubContext;
        public AuthController(IAuthorizationManager authorizationManager, IHubContext<SignalHub> hubContext)
        {
            this.authorizationManager = authorizationManager ?? throw new ArgumentNullException(nameof(authorizationManager));
            this.hubContext = hubContext;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserQuery loginData)
        {
            try
            {
                var result = await authorizationManager.LoginAsync(loginData.Email, loginData.Password);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Authorize]
        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            await hubContext.Clients.All.SendAsync("ReceiveMessage");
            return Ok();
        }
    }
}
