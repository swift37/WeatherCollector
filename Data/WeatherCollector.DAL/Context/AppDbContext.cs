using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DataSource> DataSources { get; set; }

        public DbSet<DataValue> DataValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataSource>()
                .HasMany<DataValue>()
                .WithOne(v => v.Source)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
