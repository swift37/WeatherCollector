using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherCollector.Interfaces.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers.Base
{
    public abstract class MappedNamedEntityController<T, TBase> : MappedEntityController<T, TBase> 
        where T : INamedEntity
        where TBase : INamedEntity
    {
        private readonly INamedRepository<TBase> _repository;

        public MappedNamedEntityController(INamedRepository<TBase> repository, IMapper mapper) 
            : base(repository, mapper) => _repository = repository;

        /// <summary>
        /// Get true if entity exists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/exist/name
        /// </remarks>
        /// <param name="name">Entity name</param>
        /// <returns>Returns bool</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("exist/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Exist(string? name) =>
            await _repository.Exist(name) ? Ok(true) : NotFound(false);

        /// <summary>
        /// Get the entity by name.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/name
        /// </remarks>
        /// <param name="name">Entity name</param>
        /// <returns>Returns T or entity name</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string? name)
        {
            var entity = GetEntity(await _repository.Get(name));

            return entity is null ? NotFound(name) : Ok(entity);
        }

        /// <summary>
        /// Delete the entity by name.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /entities/name
        /// </remarks>
        /// <param name="name">Entity name</param>
        /// <returns>Returns T or entity name</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string? name)
        {
            if (GetEntity(await _repository.Delete(name)) is not { } deletedEntity)
                return NotFound(name);

            return Ok(deletedEntity);
        }
    }
}
