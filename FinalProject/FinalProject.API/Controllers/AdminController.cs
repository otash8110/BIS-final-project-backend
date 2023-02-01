using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
    }
}
