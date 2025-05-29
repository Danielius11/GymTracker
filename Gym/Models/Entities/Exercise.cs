using Microsoft.AspNetCore.Mvc;

namespace Gym.Models.Entities
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Day Day { get; set; }

        public string Name { get; set; }
        public int Set { get; set; }
        public int Rep { get; set; }
        public int Kg { get; set; }
        public string? Description { get; set; }
    }

}
