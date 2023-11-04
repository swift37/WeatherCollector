using AutoMapper;
using MetaWeather.Models;
using WeatherCollector.Domain;

namespace WeatherCollector.BlazorUI.Infrastructure.Mapping
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile() => CreateMap<GeoData, City>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.City))
            .ReverseMap();
    }
}
