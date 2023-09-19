using Microsoft.AspNetCore.Mvc;

namespace NZWalk.Frontend.Controllers
{
    public class WalkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
