using System.ComponentModel.DataAnnotations;

namespace WeatherCollector.BlazorUI.Models.City
{
    public class CityCreateDTO
    {
        [Required(ErrorMessage = "The city name is required.")]
        public string? Name { get; set; }

        public string? Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
