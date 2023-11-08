using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [Produces("application/json")]
    public class SourcesRepositoryController : MappedEntityController<Source, DataSource>
    {
        public SourcesRepositoryController(IRepository<DataSource> repository, IMapper mapper) 
            : base(repository, mapper) { }
    }
}
