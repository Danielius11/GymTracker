using System.ComponentModel.DataAnnotations;

namespace Gym.Models.Entities
{
    public class Day
    {
        public Guid Id { get; set; }
        public int Nr { get; set; }
        public string Type { get; set; }
        public string Exercise { get; set; }
        public int Set { get; set; }
        public int Rep { get; set; }
        public int Kg { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
