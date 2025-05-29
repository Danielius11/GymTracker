using Gym.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Day> Days { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                .HasMany(d => d.Exercises)
                .WithOne(e => e.Day)
                .HasForeignKey(e => e.DayId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
