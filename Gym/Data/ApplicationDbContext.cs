using Gym.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext
               (DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Day> Days { get; set; }
    }
}
