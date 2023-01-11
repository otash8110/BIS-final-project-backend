using FinalProject.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthorizationManager _authorizationManager;
        public AuthController(IAuthorizationManager authorizationManager)
        {
            _authorizationManager = authorizationManager ?? throw new ArgumentNullException(nameof(authorizationManager));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginData)
        {
            var result = await _authorizationManager.LoginAsync(loginData.Email, loginData.Password);
            return Ok(result);
        }
    }
}
