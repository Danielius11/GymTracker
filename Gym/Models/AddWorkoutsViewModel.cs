namespace Gym.Models
{
    public class AddDayWithExercisesViewModel
    {
        public Guid Id { get; set; }
        public int Nr { get; set; }
        public string Type { get; set; }
        public List<ExerciseInputModel> Exercises { get; set; } = new List<ExerciseInputModel>();

    }

    public class ExerciseInputModel
    {
        public string Name { get; set; }
        public int Set { get; set; }
        public int Rep { get; set; }
        public int Kg { get; set; }
        public string? Description { get; set; }
    }
}
