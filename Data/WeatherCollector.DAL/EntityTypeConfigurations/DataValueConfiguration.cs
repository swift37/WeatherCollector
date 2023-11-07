using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL.EntityTypeConfigurations
{
    public class DataValueConfiguration : IEntityTypeConfiguration<DataValue>
    {
        public void Configure(EntityTypeBuilder<DataValue> builder)
        {
            builder.HasIndex(v => v.Time);

            builder.Navigation(v => v.Object).AutoInclude();
            builder.Navigation(v => v.Source).AutoInclude();
        }
    }
}
