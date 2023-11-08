using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.BlazorUI.Models.Source
{
    public class SourceCreateDTO
    {
        [Required(ErrorMessage = "The source name is required.")]
        public string? Name { get; set; }

        public string? Details { get; set; }
    }
}
