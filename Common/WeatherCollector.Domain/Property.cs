using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Domain
{
    public class Property: INamedEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string? Value { get; set; }

        public Source? Source { get; set; }

        public bool IsFault { get; set; }
    }
}
