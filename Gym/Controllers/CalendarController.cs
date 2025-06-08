using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CalendarController : Controller
{
    private readonly ApplicationDbContext dbcontext;

    public CalendarController(ApplicationDbContext context)
    {
        dbcontext = context;
    }

    public async Task<IActionResult> Index(int? year, int? month)
    {
        var today = DateTime.Today;
        int y = year ?? today.Year;
        int m = month ?? today.Month;

        var daysInMonth = DateTime.DaysInMonth(y, m);
        var firstDayOfMonth = new DateTime(y, m, 1);

        var workouts = await dbcontext.Days
            .Where(d => d.Date.Year == y && d.Date.Month == m)
            .ToListAsync();

        var model = new CalendarViewModel
        {
            Year = y,
            Month = m,
            DaysWithWorkouts = workouts.ToDictionary(d => d.Date.Day, d => d.Id),
            Days = workouts
        };

        return View(model);
    }
}
