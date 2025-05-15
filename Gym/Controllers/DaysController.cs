using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class DaysController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
