using Microsoft.AspNetCore.Mvc;

namespace NZWalk.Frontend.Controllers
{
    public class DifficultyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
