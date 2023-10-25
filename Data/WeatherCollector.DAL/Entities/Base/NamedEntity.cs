using System.ComponentModel.DataAnnotations;
using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.DAL.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string? Name { get; set; }
    }
}
