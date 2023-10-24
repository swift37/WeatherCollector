using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.Interfaces.Base
{
    public interface IEntity
    {
        [Required]
        int Id { get; set; }
    }
}
