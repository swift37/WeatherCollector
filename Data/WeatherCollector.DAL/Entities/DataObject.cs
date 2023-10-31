using WeatherCollector.DAL.Entities.Base;

namespace WeatherCollector.DAL.Entities
{
    public class DataObject : NamedEntity
    {
        public string? Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
