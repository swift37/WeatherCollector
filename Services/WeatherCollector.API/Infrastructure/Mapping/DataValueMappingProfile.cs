using AutoMapper;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;

namespace WeatherCollector.API.Infrastructure.Mapping
{
    public class DataValueMappingProfile: Profile
    {
        public DataValueMappingProfile() => CreateMap<Property, DataValue>()
            .ForMember(dest => dest.Object, act => act.MapFrom(src => src.City))
            .ReverseMap();

    }
}
