using System.ComponentModel.DataAnnotations;

namespace Gym.Models.Entities
{
    public class Day
    {
        public Guid Id { get; set; }
        public int Nr { get; set; }
        public string Type { get; set; }

        public DateTime Date { get; set; }

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }

}

