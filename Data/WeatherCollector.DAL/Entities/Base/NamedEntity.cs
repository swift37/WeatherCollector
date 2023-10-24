using WeatherCollector.Interfaces.Base;

namespace WeatherCollector.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        public string? Name { get; set; }
    }
}
