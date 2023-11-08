using WeatherCollector.BlazorUI.Models.City;
using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Services.Interfaces
{
    public interface ICitiesService
    {
        Task<Response<IEnumerable<City>>> GetAll();

        Task<Response<City>> Get(int id);

        Task<Response<City>> Get(string? name);

        Task<Response<City>> GetOrCreate(string? name);

        Task<Response<City>> Create(CityCreateDTO cityCreateDTO);

        Task<Response<City>> Update(CityUpdateDTO cityUpdateDTO);

        Task<Response<City>> Delete(City city);
    }
}
