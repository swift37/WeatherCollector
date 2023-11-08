using WeatherCollector.BlazorUI.Models.Source;
using WeatherCollector.BlazorUI.Services.Base;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Services.Interfaces
{
    public interface ISourcesService
    {
        Task<Response<IEnumerable<Source>>> GetAll();

        Task<Response<Source>> Get(int id);

        Task<Response<Source>> Create(SourceCreateDTO sourceCreateDTO);

        Task<Response<Source>> Update(SourceUpdateDTO sourceUpdateDTO);

        Task<Response<Source>> Delete(Source source);
    }
}
