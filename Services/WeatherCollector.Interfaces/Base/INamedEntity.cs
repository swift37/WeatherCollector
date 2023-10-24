using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.Interfaces.Base
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string? Name { get; set; }
    }
}
