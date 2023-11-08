using MetaWeather;
using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.BlazorUI.Services.Interfaces;
using WeatherCollector.Clients.Repositories;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly PropertiesWebRepository _propertiesRepository;
        private readonly ICitiesService _citiesService;
        private readonly AstroWeatherClient _client;

        public WeatherService(
            PropertiesWebRepository propertiesRepository,
            ICitiesService citiesService,
            AstroWeatherClient client) => 
            (_propertiesRepository, _citiesService, _client) = 
            (propertiesRepository, citiesService, client);

        public async Task<Response<IEnumerable<Property>>> GetCurrentWeather(string? cityName)
        {
            var response = await _citiesService.GetOrCreate(cityName);

            if (!response.Success) return 
                    new Response<IEnumerable<Property>> { Success = false, FaultMessage = response.FaultMessage };

            var city = response.Data;

            var currentWeather = await _client.GetCurrentWeatherData(city.Latitude, city.Longitude);

            if (currentWeather is not { }) return
                    new Response<IEnumerable<Property>> { Success = false, FaultMessage = "There is currently no available meteorological information for this city." };

            ICollection<Property> properties = new HashSet<Property>();

            await foreach (var property in GetProperties(currentWeather, city))
                properties.Add(property);
                
            return new Response<IEnumerable<Property>> { Data = properties };
        }

        /// <summary>
        /// It is a widely applicable method for separating data objects 
        /// that contain nested components into distinct ones. 
        /// This results in an enumeration with associated properties.
        /// </summary>
        /// <param name="data">Any data object</param>
        /// <param name="city">City</param>
        /// <returns>IAsyncEnumerable<Property></returns>
        private async IAsyncEnumerable<Property> GetProperties(object data, City city)
        {
            foreach (var item in data.GetType().GetProperties())
            {
                if (item.PropertyType.IsValueType || item.PropertyType == typeof(string))
                {
                    var createdProp = await _propertiesRepository.Create(new Property
                    {
                        Time = DateTimeOffset.Now,
                        City = city,
                        Name = item.Name,
                        Value = item.GetValue(data)?.ToString(),
                        IsFault = item.GetValue(data) is null ? true : false,
                    });
                    
                    if (createdProp is { })
                        yield return createdProp;
                }
                else if (item.GetValue(data) is ICollection<object> collection)
                {
                    foreach (var obj in collection)
                        await foreach (var result in GetProperties(obj, city))
                            yield return result;
                }
                else
                {
                    var parentObject = item.GetValue(data);

                    if (parentObject is { })
                        foreach (var nestedItem in item.PropertyType.GetProperties())
                        {
                            var createdProp = await _propertiesRepository.Create(new Property
                            {
                                Time = DateTimeOffset.Now,
                                City = city,
                                Name = nestedItem.Name,
                                Value = nestedItem.GetValue(parentObject)?.ToString(),
                                IsFault = nestedItem.GetValue(parentObject) is null ? true : false
                            });

                            if (createdProp is { })
                                yield return createdProp;
                        }
                }
            }
        }

        public async Task<Response<IEnumerable<Property>>> GetCollectedData(string? cityName)
        {
            var response = await _citiesService.Get(cityName);

            if (!response.Success) return
                    new Response<IEnumerable<Property>> { Success = false, FaultMessage = response.FaultMessage };

            var city = response.Data;

            var properties = await _propertiesRepository.GetAllMatches(city.Name);

            if (properties is not { }) return
                    new Response<IEnumerable<Property>> { Success = false, FaultMessage = "There is no collected data about this city." };

            return new Response<IEnumerable<Property>> { Data = properties };
        }
    }
}
