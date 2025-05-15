using Gym.Data;
using Gym.Models;
using Gym.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class DaysController : Controller
    {
        private readonly ApplicationDbContext dbcontext;

        public DaysController(ApplicationDbContext dbcontext) 
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWorkoutsViewModel viewmodel)
        {
            var day = new Day

            {
                Nr = viewmodel.Nr,
                Type = viewmodel.Type,
                Exercise = viewmodel.Exercise,
                Set = viewmodel.Set,
                Rep = viewmodel.Rep,
                Kg = viewmodel.Kg,
                Description = viewmodel.Description
            };

            await dbcontext.Days.AddAsync(day);
            dbcontext.SaveChanges();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var days = await dbcontext.Days.ToListAsync();

            return View(days);
        }
    }
}
