using AutoMapper;
using MetaWeather;
using WeatherCollector.BlazorUI.Models.Source;
using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.BlazorUI.Services.Interfaces;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.BlazorUI.Services
{
    public class SourcesService : ISourcesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Source> _sourcesRepository;

        public SourcesService(IMapper mapper, IRepository<Source> sourcesRepository, AstroWeatherClient client) =>
            (_mapper, _sourcesRepository) = (mapper, sourcesRepository);

        public async Task<Response<IEnumerable<Source>>> GetAll()
        {
            var sources = await _sourcesRepository.GetAll();
            return new Response<IEnumerable<Source>>() { Data = sources };
        }

        public async Task<Response<Source>> Get(int id)
        {
            var source = await _sourcesRepository.Get(id);

            return source is null ?
                new Response<Source>() { Success = false, FaultMessage = "The source is not found." } :
                new Response<Source>() { Data = source };
        }

        public async Task<Response<Source>> Create(SourceCreateDTO sourceCreateDTO)
        {
            var source = _mapper.Map<Source>(sourceCreateDTO);

            var createdSource = await _sourcesRepository.Create(source);
            return createdSource is null ?
                new Response<Source>() { Success = false, FaultMessage = "An error occurred during creating the source." } :
                new Response<Source>() { Data = createdSource };
        }

        public async Task<Response<Source>> Update(SourceUpdateDTO sourceUpdateDTO)
        {
            var source = _mapper.Map<Source>(sourceUpdateDTO);

            var updatedSource = await _sourcesRepository.Update(source);
            return updatedSource is null ?
                new Response<Source>() { Success = false, FaultMessage = "An error occurred during updating the source." } :
                new Response<Source>() { Data = updatedSource };
        }

        public async Task<Response<Source>> Delete(Source source)
        {
            var deletedSource = await _sourcesRepository.Delete(source);
            return deletedSource is null ?
                new Response<Source>() { Success = false, FaultMessage = "An error occurred during deleting the source." } :
                new Response<Source>() { Data = deletedSource };
        }
    }
}
