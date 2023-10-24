using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL.EntityTypeConfigurations
{
    public class DataSourceConfiguration : IEntityTypeConfiguration<DataSource>
    {
        public void Configure(EntityTypeBuilder<DataSource> builder)
        {
            builder
                .HasMany<DataValue>()
                .WithOne(v => v.Source)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
