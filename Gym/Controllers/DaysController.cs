using Gym.Models;
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

        [HttpPost]
        public IActionResult Add(AddWorkoutsViewModel viewmodel)
        {
            
            if (viewmodel is null)
            {
                throw new ArgumentNullException(nameof(viewmodel));
            }

            return View();
        }
    }
}
