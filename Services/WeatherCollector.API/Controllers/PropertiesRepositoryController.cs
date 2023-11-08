using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.DAL.Repositories;
using WeatherCollector.Domain;

namespace WeatherCollector.API.Controllers
{
    [Produces("application/json")]
    public class PropertiesRepositoryController : MappedEntityController<Property, DataValue>
    {
        private new readonly DataValuesRepository _repository;

        public PropertiesRepositoryController(DataValuesRepository repository, IMapper mapper)
            : base(repository, mapper) => _repository = repository;

        /// <summary>
        /// Get a enumeration of entities with matching names.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /entities/name
        /// </remarks>
        /// <returns>Returns IEnumerable<Property></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Property>>> GetAllMatches(string? name)
        {
            var entities = GetEntities(await _repository.GetAllMatches(name));

            return entities is null ? NotFound(Enumerable.Empty<Property>()) : Ok(entities);
        }
    }
}
