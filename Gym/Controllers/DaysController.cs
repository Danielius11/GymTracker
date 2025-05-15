using Gym.Data;
using Gym.Models;
using Gym.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var day = await dbcontext.Days.FindAsync(id);

            return View(day);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Day viewmodel)
        {
            var day = await dbcontext.Days.FindAsync(viewmodel.Id);

            if (day is not null)
            {
                day.Nr = viewmodel.Nr;
                day.Type = viewmodel.Type;
                day.Exercise = viewmodel.Exercise;
                day.Set = viewmodel.Set;
                day.Rep = viewmodel.Rep;
                day.Kg = viewmodel.Kg;
                day.Description = viewmodel.Description;

                await dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Days");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Day viewmodel)
        {
            var day = await dbcontext.Days
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewmodel.Id);

            if (day is not null)
            {
                dbcontext.Days.Remove(viewmodel);
                await dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Days");
        }
    }
}
