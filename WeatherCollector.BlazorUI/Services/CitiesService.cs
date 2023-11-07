using AutoMapper;
using MetaWeather;
using WeatherCollector.BlazorUI.Models.City;
using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.BlazorUI.Services.Interfaces;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.BlazorUI.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly IMapper _mapper;
        private readonly INamedRepository<City> _citiesRepository;
        private readonly AstroWeatherClient _client;

        public CitiesService(IMapper mapper, INamedRepository<City> cityRepository, AstroWeatherClient client) =>
            (_client, _mapper, _citiesRepository) = (client, mapper, cityRepository);

        public async Task<Response<IEnumerable<City>>> GetAll()
        {
            var cities = await _citiesRepository.GetAll();
            return new Response<IEnumerable<City>>() { Data = cities };
        }

        public async Task<Response<City>> Get(int id)
        {
            var city = await _citiesRepository.Get(id);

            return city is null ? 
                new Response<City>() { Success = false, FaultMessage = "The city is not found." } :
                new Response<City>() { Data = city };
        }

        public async Task<Response<City>> GetOrCreate(string? name)
        {
            var city = await _citiesRepository.Get(name);
            if (city is { })
                return new Response<City> { Data = city };

            var geoData = await _client.GetGeoData(name);

            if (geoData is not { Length: > 0 })
                return new Response<City> { Success = false, FaultMessage = "There is no city with this name." };

            city = _mapper.Map<City>(geoData[0]);

            var createdCity = await _citiesRepository.Create(city);
            return createdCity is null ?
                new Response<City>() { Success = false, FaultMessage = "An error occurred during creating an object." } :
                new Response<City>() { Data = createdCity };
        }

        public async Task<Response<City>> Create(CityCreateDTO cityCreateDTO)
        {
            var geoData = await _client.GetGeoData(cityCreateDTO.Name);

            City city;
            if (geoData is not { Length: > 0 })
            {
                if (cityCreateDTO.Latitude == default || cityCreateDTO.Longitude == default)
                    return new Response<City> { Success = false, FaultMessage = "There is no city with this name." };

                city = _mapper.Map<City>(cityCreateDTO);
            }
            else
                city = _mapper.Map<City>(geoData[0]);

            var createdCity = await _citiesRepository.Create(city);
            return createdCity is null ?
                new Response<City>() { Success = false, FaultMessage = "An error occurred during creating an object." } :
                new Response<City>() { Data = createdCity };
        }

        public async Task<Response<City>> Update(CityUpdateDTO cityUpdateDTO)
        {
            var city = _mapper.Map<City>(cityUpdateDTO);

            var updatedCity = await _citiesRepository.Update(city);
            return updatedCity is null ?
                new Response<City>() { Success = false, FaultMessage = "An error occurred during updating an object." } :
                new Response<City>() { Data = updatedCity };
        }

        public async Task<Response<City>> Delete(City city)
        {
            var deletedCity = await _citiesRepository.Delete(city);
            return deletedCity is null ?
                new Response<City>() { Success = false, FaultMessage = "An error occurred during deleting an object." } :
                new Response<City>() { Data = deletedCity };
        }
    }
}
