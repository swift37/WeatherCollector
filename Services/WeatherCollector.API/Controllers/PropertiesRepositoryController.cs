using AutoMapper;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    public class PropertiesRepositoryController : MappedEntityController<Property, DataValue>
    {
        public PropertiesRepositoryController(IRepository<DataValue> repository, IMapper mapper)
            : base(repository, mapper) { }
    }
}
