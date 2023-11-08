using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.BlazorUI.Models.Source
{
    public class SourceUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The source name is required.")]
        public string? Name { get; set; }

        public string? Details { get; set; }
    }
}
