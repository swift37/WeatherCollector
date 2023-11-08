using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WeatherCollector.DAL.Entities;

namespace WeatherCollector.DAL.EntityTypeConfigurations
{
    public class DataObjectConfiguration : IEntityTypeConfiguration<DataObject>
    {
        public void Configure(EntityTypeBuilder<DataObject> builder)
        {
            builder
                .HasMany<DataValue>()
                .WithOne(v => v.Object)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => s.Name);
        }
    }
}
