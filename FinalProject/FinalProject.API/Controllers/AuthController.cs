using Azure.Core;
using FinalProject.Application.Common.DTOs;
using FinalProject.Application.Common.Interfaces;
using FinalProject.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserSercive authorizationManager;
        private readonly IHubContext<SignalHub> hubContext;
        private readonly IMediator mediator;
        private readonly IConfiguration configuration;

        public AuthController(IUserSercive authorizationManager, IHubContext<SignalHub> hubContext, IMediator mediator, IConfiguration configuration)
        {
            this.authorizationManager = authorizationManager ?? throw new ArgumentNullException(nameof(authorizationManager));
            this.hubContext = hubContext;
            this.mediator = mediator;
            this.configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUser loginData)
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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUser registerData, CancellationToken cancellationToken)
        {
            try
            {
                Enum.TryParse<Roles>(registerData.Role, out var role);
                var result = await authorizationManager.CreateUserAsync(registerData.Email, registerData.Name, registerData.Surname, registerData.Password, role);
                await hubContext.Clients.Group(configuration["AdminsGroup"]).SendAsync("RegistrationRequest", registerData.Email);
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
