using FinalProject.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "Distributor,Admin", Policy = "IsRegistrationApproved")]
    public class OffersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
