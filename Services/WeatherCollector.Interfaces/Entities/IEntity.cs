using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.Interfaces.Entities
{
    public interface IEntity
    {
        [Required]
        int Id { get; set; }
    }
}
