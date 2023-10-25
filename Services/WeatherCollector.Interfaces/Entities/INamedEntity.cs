using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.Interfaces.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string? Name { get; set; }
    }
}
