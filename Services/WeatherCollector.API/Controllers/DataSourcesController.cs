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
        public async Task<ActionResult<int>> GetDataSourcesCount() => Ok(await _repository.GetCount());

        /// <summary>
        /// Get true if data source exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/exist/1
        /// </remarks>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("exist/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<ActionResult<bool>> ExistId(int id) => 
            await _repository.ExistById(id) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get true if data source exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/exist
        /// {
        ///     Id: 1,
        ///     Name: "Source",
        ///     Details: "Details"
        /// }
        /// </remarks>
        /// <param name="dataSource">DataSource</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("exist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Exist(DataSource dataSource) =>
            await _repository.Exist(dataSource) ? Ok(true) : NotFound(false);

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
        /// <param name="id">Data source id</param>
        /// <returns>Returns IEnumerable<DataSource></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataSource))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DataSource>> Get(int id)
        {
            var source = await _repository.Get(id);

            return source is null ? NotFound() : Ok(source); 
        }

        /// <summary>
        /// Get a list of data sources with the specified number of elements, skipping the specified number of elements.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/sources
        /// </remarks>
        /// <param name="skip">Number of passable elements</param>
        /// <param name="count">Number of elements to be received</param>
        /// <returns>Returns IEnumerable<DataSource></returns>
        /// <response code="200">Success</response>
        [HttpGet("items[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DataSource>))]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count) =>
            Ok(await _repository.Get(skip, count));

        /// <summary>
        /// Get a page with the specified number of data sources.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /datasources/page/1/3
        /// </remarks>
        /// <param name="index">Page index</param>
        /// <param name="size">Page size</param>
        /// <returns>Returns IPage<DataSource></returns>
        /// <response code="200">Success</response>
        [HttpGet("page/{index:int}/{size:int}")]
        [HttpGet("page[[{index:int}:{size:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPage<DataSource>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IPage<DataSource>))]
        public async Task<ActionResult<IPage<DataSource>>> GetPage(int index, int size)
        {
            var page = await _repository.GetPage(index, size);

            return page.Items.Any() ? Ok(page) : NotFound(page);
        }

        /// <summary>
        /// Create a data source
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /datasources
        /// {
        ///     Name: "Source",
        ///     Details: "Details"
        /// }
        /// </remarks>
        /// <param name="dataSource">DataSource</param>
        /// <returns>Returns DataSource</returns>
        /// <response code="201">Created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataSource))]
        public async Task<ActionResult<DataSource>> Create([FromBody] DataSource dataSource)
        {
            var createdSource = await _repository.Create(dataSource);

            return CreatedAtAction(nameof(Get), new { id = createdSource?.Id });
        }

        /// <summary>
        /// Update a data source
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /datasources
        /// {
        ///     Id: 1,
        ///     Name: "Modified Source",
        ///     Details: "Modified Details"
        /// }
        /// </remarks>
        /// <param name="dataSource">DataSource</param>
        /// <returns>Returns DataSource</returns>
        /// <response code="202">Accepted</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(DataSource))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DataSource>> Update([FromBody] DataSource dataSource)
        {
            if (await _repository.Update(dataSource) is not { } updatedSource) 
                return NotFound();

            return AcceptedAtAction(nameof(Get), new { id = updatedSource.Id });
        }

        /// <summary>
        /// Delete a data source
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /datasources
        /// {
        ///     Id: 1,
        ///     Name: "Modified Source",
        ///     Details: "Modified Details"
        /// }
        /// </remarks>
        /// <param name="dataSource">DataSource</param>
        /// <returns>Returns DataSource</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataSource))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(DataSource))]
        public async Task<ActionResult<DataSource>> Delete([FromBody] DataSource dataSource)
        {
            if (await _repository.Delete(dataSource) is not { } deletedSource)
                return NotFound(dataSource);

            return Ok(deletedSource);
        }

        /// <summary>
        /// Delete a data source
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /datasources/1
        /// </remarks>
        /// <param name="id">Source id</param>
        /// <returns>Returns DataSource or source id</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataSource))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteById(id) is not { } deletedSource)
                return NotFound(id);

            return Ok(deletedSource);
        }
    }
}
