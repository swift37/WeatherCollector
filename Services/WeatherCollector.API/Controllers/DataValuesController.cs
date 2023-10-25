using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [Produces("application/json")]
    public class DataValuesController : EntityController<DataValue>
    {
        public DataValuesController(IRepository<DataValue> repository) : base(repository) { }
    }
}
