using Microsoft.AspNetCore.Mvc;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourcesController : Controller
    {
        private readonly IRepository<DataSource> _repository;

        public DataSourcesController(IRepository<DataSource> repository) => _repository = repository;

        [HttpGet("count")]
        public async Task<IActionResult> GetItemsCount() => Ok(await _repository.GetCount());
    }
}
