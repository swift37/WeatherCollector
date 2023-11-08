using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Entities;
using WeatherCollector.DAL.EntityTypeConfigurations;

namespace WeatherCollector.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DataObject> DataObjects { get; set; }

        public DbSet<DataSource> DataSources { get; set; }

        public DbSet<DataValue> DataValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DataObjectConfiguration());
            modelBuilder.ApplyConfiguration(new DataSourceConfiguration());
            modelBuilder.ApplyConfiguration(new DataValueConfiguration());
        }
    }
}
