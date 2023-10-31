using AutoMapper;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    public class CitiesRepositoryController : MappedEntityController<City, DataObject>
    {
        public CitiesRepositoryController(IRepository<DataObject> repository, IMapper mapper) 
            : base(repository, mapper) { }
    }
}
