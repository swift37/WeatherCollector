using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<Response<IEnumerable<Property>>> GetCurrentWeather(string? cityName);

        Task<Response<IEnumerable<Property>>> GetCollectedData(string? cityName);
    }
}
