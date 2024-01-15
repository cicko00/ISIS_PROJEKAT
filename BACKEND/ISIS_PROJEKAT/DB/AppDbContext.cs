using ISIS_PROJEKAT.Models;
using Microsoft.EntityFrameworkCore;

namespace ISIS_PROJEKAT.DB
{
    public class AppDbContext:DbContext
    {
        public DbSet<LoadDataHistory> LoadDatasHistory { get; set; }
        public DbSet<LoadDataPrediction> LoadDatasPrediction { get; set; }
        public DbSet<WheatherForecast> WheatherForecasts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here
        }
    }
}
