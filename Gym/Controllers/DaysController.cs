﻿using Gym.Data;
using Gym.Models;
using Gym.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class DaysController : Controller
    {
        private readonly ApplicationDbContext dbcontext;

        public DaysController(ApplicationDbContext context)
        {
            dbcontext = context;
        }

        [HttpGet]
        public IActionResult Add(DateTime? date)
        {
            var model = new AddDayWithExercisesViewModel
            {
                Date = date ?? DateTime.Today
            };
            model.Exercises.Add(new ExerciseInputModel());
            return View("Add", model);
        }



        [HttpPost("Days/Add")]
        public async Task<IActionResult> Add(AddDayWithExercisesViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Date == default)
                model.Date = DateTime.Today;

            bool dateExists = await dbcontext.Days.AnyAsync(d => d.Date.Date == model.Date.Date);
            if (dateExists)
            {
                ModelState.AddModelError("", "This day already has a workout");
                return View(model);
            }

            var day = new Day
            {
                Id = Guid.NewGuid(),
                Nr = model.Nr,
                Type = model.Type,
                Date = model.Date,
                Exercises = model.Exercises.Select(e => new Exercise
                {
                    Id = Guid.NewGuid(),
                    Name = e.Name,
                    Set = e.Set,
                    Rep = e.Rep,
                    Kg = e.Kg,
                    Description = e.Description
                }).ToList()
            };

            dbcontext.Days.Add(day);
            await dbcontext.SaveChangesAsync();

            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var days = await dbcontext.Days
                .Include(d => d.Exercises)
                .ToListAsync();

            return View(days);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var day = await dbcontext.Days
                .Include(d => d.Exercises)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (day == null)
                return NotFound();

            var model = new AddDayWithExercisesViewModel
            {
                Id = day.Id,
                Nr = day.Nr,
                Type = day.Type,
                Date = day.Date,
                Exercises = day.Exercises.Select(e => new ExerciseInputModel
                {
                    Name = e.Name,
                    Set = e.Set,
                    Rep = e.Rep,
                    Kg = e.Kg,
                    Description = e.Description
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AddDayWithExercisesViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingDay = await dbcontext.Days
                .FirstOrDefaultAsync(d => d.Date == model.Date && d.Id != id);

            if (existingDay != null)
            {
                ModelState.AddModelError("Date", "This day already has a workout");
                return View(model);
            }

            var day = await dbcontext.Days
                .Include(d => d.Exercises)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (day == null)
                return NotFound();

            day.Nr = model.Nr;
            day.Type = model.Type;
            day.Date = model.Date;

            dbcontext.Exercises.RemoveRange(day.Exercises);

            day.Exercises = model.Exercises.Select(e => new Exercise
            {
                Id = Guid.NewGuid(),
                DayId = day.Id,
                Name = e.Name,
                Set = e.Set,
                Rep = e.Rep,
                Kg = e.Kg,
                Description = e.Description
            }).ToList();

            dbcontext.Exercises.AddRange(day.Exercises);

            await dbcontext.SaveChangesAsync();

            return RedirectToAction("List");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var day = await dbcontext.Days.FindAsync(id);
            if (day != null)
            {
                dbcontext.Days.Remove(day);
                await dbcontext.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
