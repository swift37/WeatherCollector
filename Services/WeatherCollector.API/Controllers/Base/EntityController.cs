using Microsoft.AspNetCore.Mvc;
using WeatherCollector.Interfaces.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityController<T> : ControllerBase where T : IEntity
    {
        private readonly IRepository<T> _repository;

        public EntityController(IRepository<T> repository) => _repository = repository;

        /// <summary>
        /// Get count of entities
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/count
        /// </remarks>
        /// <returns>Returns int</returns>
        /// <response code="200">Success</response>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> GetItemsCount() => Ok(await _repository.GetCount());

        /// <summary>
        /// Get true if entity exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/exist/1
        /// </remarks>
        /// <param name="id">Entity id</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("exist/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> ExistId(int id) =>
            await _repository.ExistById(id) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get true if entity exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /entities/exist
        /// {
        ///     Id: 1,
        ///     Name: "Entity",
        ///     Details: "Details"
        /// }
        /// </remarks>
        /// <param name="entity">T</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpPost("exist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(T entity) =>
            await _repository.Exist(entity) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get a enumeration of entities
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities
        /// </remarks>
        /// <returns>Returns IEnumerable<T></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> GetAll() => Ok(await _repository.GetAll());

        /// <summary>
        /// Get a enumeration of entities with the specified number of elements, skipping the specified number of elements.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/1
        /// </remarks>
        /// <param name="id">Entity id</param>
        /// <returns>Returns IEnumerable<T></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> Get(int id)
        {
            var entity = await _repository.Get(id);

            return entity is null ? NotFound() : Ok(entity);
        }

        /// <summary>
        /// Get a enumeration of entities with the specified number of elements, skipping the specified number of elements.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/items[3:5]
        /// </remarks>
        /// <param name="skip">Number of passable elements</param>
        /// <param name="count">Number of elements to be received</param>
        /// <returns>Returns IEnumerable<T></returns>
        /// <response code="200">Success</response>
        [HttpGet("items[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<T>>> Get(int skip, int count) =>
            Ok(await _repository.Get(skip, count));

        /// <summary>
        /// Get a page with the specified number of entities.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/page/1/3
        /// </remarks>
        /// <param name="index">Page index</param>
        /// <param name="size">Page size</param>
        /// <returns>Returns IPage<T></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("page/{index:int}/{size:int}")]
        [HttpGet("page[[{index:int}:{size:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<T>>> GetPage(int index, int size)
        {
            var page = await _repository.GetPage(index, size);

            return page.Items.Any() ? Ok(page) : NotFound(page);
        }

        /// <summary>
        /// Create an entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /entities
        /// {
        ///     Name: "Entity",
        ///     Details: "Details"
        /// }
        /// </remarks>
        /// <param name="entity">T</param>
        /// <returns>Returns T</returns>
        /// <response code="201">Created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<T>> Create([FromBody] T entity)
        {
            var createdEntity = await _repository.Create(entity);

            return CreatedAtAction(nameof(Get), new { id = createdEntity?.Id }, entity);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /entities
        /// {
        ///     Id: 1,
        ///     Name: "Modified Entity",
        ///     Details: "Modified Details"
        /// }
        /// </remarks>
        /// <param name="entity">T</param>
        /// <returns>Returns T</returns>
        /// <response code="202">Accepted</response>
        /// <response code="404">Not Found</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> Update([FromBody] T entity)
        {
            if (await _repository.Update(entity) is not { } updatedEntity)
                return NotFound();

            return AcceptedAtAction(nameof(Get), new { id = updatedEntity.Id }, updatedEntity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /entities
        /// {
        ///     Id: 1,
        ///     Name: "Modified Entity",
        ///     Details: "Modified Details"
        /// }
        /// </remarks>
        /// <param name="entity">T</param>
        /// <returns>Returns T</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<T>> Delete([FromBody] T entity)
        {
            if (await _repository.Delete(entity) is not { } deletedEntity)
                return NotFound(entity);

            return Ok(deletedEntity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /entities/1
        /// </remarks>
        /// <param name="id">Entity id</param>
        /// <returns>Returns T or entity id</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteById(id) is not { } deletedEntity)
                return NotFound(id);

            return Ok(deletedEntity);
        }
    }
}
