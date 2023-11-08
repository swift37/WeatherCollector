using WeatherCollector.BlazorUI.Models.City;
using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Services.Interfaces
{
    public interface IPropertiesService
    {
        Task<Response<IEnumerable<Property>>> GetAll();

        Task<Response<Property>> Get(int id);

        Task<Response<Property>> Delete(Property property);
    }
}
