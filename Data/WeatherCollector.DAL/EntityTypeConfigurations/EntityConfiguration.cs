using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherCollector.DAL.Entities.Base;

namespace WeatherCollector.DAL.EntityTypeConfigurations
{
    internal class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id).IsUnique();
        }
    }
}
