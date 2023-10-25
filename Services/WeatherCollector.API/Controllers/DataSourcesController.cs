using Microsoft.AspNetCore.Mvc;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DataSourcesController : Controller
    {
        private readonly IRepository<DataSource> _repository;

        public DataSourcesController(IRepository<DataSource> repository) => _repository = repository;

        /// <summary>
        /// Get count of data sources
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/count
        /// </remarks>
        /// <returns>Returns int</returns>
        /// <response code="200">Success</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> GetItemsCount() => Ok(await _repository.GetCount());

        /// <summary>
        /// Get true if entity exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/exist/1
        /// </remarks>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("exist/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<ActionResult<bool>> ExistId(int id) => 
            await _repository.ExistById(id) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get a list of data sources
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources
        /// </remarks>
        /// <returns>Returns IEnumerable<DataSource></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DataSource>))]
        public async Task<ActionResult<IEnumerable<DataSource>>> GetAll() => Ok(await _repository.GetAll());

        /// <summary>
        /// Get a list of data sources with the specified number of elements, skipping the specified number of elements.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/sources
        /// </remarks>
        /// <returns>Returns IEnumerable<DataSource></returns>
        /// <response code="200">Success</response>
        [HttpGet("sources[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DataSource>))]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count) => 
            Ok(await _repository.Get(skip, count));
    }
}
