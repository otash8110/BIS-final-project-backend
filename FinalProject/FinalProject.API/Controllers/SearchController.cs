using Microsoft.AspNetCore.Mvc;

namespace FinalProject.API.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
