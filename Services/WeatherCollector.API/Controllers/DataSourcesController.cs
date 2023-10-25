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

        /// <summary>
        /// Get count of data sources
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /count
        /// </remarks>
        /// <returns>Returns int</returns>
        /// <response code="200">Success</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> GetItemsCount() => Ok(await _repository.GetCount());
    }
}
