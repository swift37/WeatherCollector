using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [Produces("application/json")]
    public class CitiesRepositoryController : MappedNamedEntityController<City, DataObject>
    {
        public CitiesRepositoryController(INamedRepository<DataObject> repository, IMapper mapper) 
            : base(repository, mapper) { }
    }
}
