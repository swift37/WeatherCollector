using AutoMapper;
using WeatherCollector.BlazorUI.Models.Source;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Infrastructure.Mapping
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<SourceCreateDTO, Source>().ReverseMap();

            CreateMap<SourceUpdateDTO, Source>().ReverseMap();
        }
    }
}
