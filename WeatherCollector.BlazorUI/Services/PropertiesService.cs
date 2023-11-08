using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.BlazorUI.Services.Interfaces;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.BlazorUI.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly IRepository<Property> _propertiesRepository;

        public PropertiesService(IRepository<Property> propertyRepository) =>
            _propertiesRepository = propertyRepository;

        public async Task<Response<IEnumerable<Property>>> GetAll()
        {
            var properties = await _propertiesRepository.GetAll();
            return new Response<IEnumerable<Property>>() { Data = properties };
        }

        public async Task<Response<Property>> Get(int id)
        {
            var property = await _propertiesRepository.Get(id);

            return property is null ?
                new Response<Property>() { Success = false, FaultMessage = "The property is not found." } :
                new Response<Property>() { Data = property };
        }

        public async Task<Response<Property>> Delete(Property property)
        {
            var deletedProperty = await _propertiesRepository.Delete(property);
            return deletedProperty is null ?
                new Response<Property>() { Success = false, FaultMessage = "An error occurred during deleting an object." } :
                new Response<Property>() { Data = deletedProperty };
        }
    }
}
