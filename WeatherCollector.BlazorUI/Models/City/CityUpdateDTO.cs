using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.BlazorUI.Models.City
{
    public class CityUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The city name is required.")]
        public string? Name { get; set; }

        public string? Country { get; set; }

        [Required(ErrorMessage = "The latitude is required.")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "The longitude is required.")]
        public double Longitude { get; set; }
    }
}
