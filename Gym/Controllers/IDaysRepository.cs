using Gym.Data;
using Gym.Models;
using Gym.Models.Entities;

namespace Gym.Controllers
{
    public interface IDaysRepository : BaseRepo<Day>
    {
        Task<Day> AddAndReturnDays(AddDayWithExercisesViewModel viewmodel);
    }

    public class DaysRepository : IDaysRepository
    {
        private readonly ApplicationDbContext dbcontext;

        public DaysRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Day> AddAndReturnDays(AddDayWithExercisesViewModel viewmodel)
        {
            var day = new Day
            {
                Id = Guid.NewGuid(),
                Nr = viewmodel.Nr,
                Type = viewmodel.Type,
                Exercises = viewmodel.Exercises.Select(e => new Exercise
                {
                    Id = Guid.NewGuid(),
                    Name = e.Name,
                    Set = e.Set,
                    Rep = e.Rep,
                    Kg = e.Kg,
                    Description = e.Description
                }).ToList()
            };

            await dbcontext.Days.AddAsync(day);
            await dbcontext.SaveChangesAsync();

            return day;
        }

        public Task Add(Day viewmodel)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Day viewmodel)
        {
            throw new NotImplementedException();
        }
    }

    public interface BaseRepo<T> where T : class
    {
        Task Add(T viewmodel);
        Task Delete(Guid id);
        Task Insert(T viewmodel);
    }
}
