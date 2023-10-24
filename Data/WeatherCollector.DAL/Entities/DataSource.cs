using WeatherCollector.DAL.Entities.Base;

namespace WeatherCollector.DAL.Entities
{
    public class DataSource : NamedEntity
    {
        public string? Details { get; set; } 
    }
}
