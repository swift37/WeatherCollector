using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL.EntityTypeConfigurations
{
    public class DataValueConfiguration : IEntityTypeConfiguration<DataValue>
    {
        public void Configure(EntityTypeBuilder<DataValue> builder)
        {
            builder.HasIndex(s => s.Time);
        }
    }
}
