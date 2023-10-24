using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string? Name { get; set; }
    }
}
