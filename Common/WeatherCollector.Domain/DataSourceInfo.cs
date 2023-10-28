using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Domain
{
    public class DataSourceInfo : INamedEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Details { get; set; }
    }
}
