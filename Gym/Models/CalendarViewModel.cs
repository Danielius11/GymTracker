using Gym.Models.Entities;

namespace Gym.Models
{
    public class CalendarViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public Dictionary<int, Guid> DaysWithWorkouts { get; set; }

        public List<Day> Days { get; set; }
    }

}
