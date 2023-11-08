using AutoMapper;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;

namespace WeatherCollector.API.Infrastructure.Mapping
{
    public class DataObjectMappingProfile : Profile
    {
        public DataObjectMappingProfile() => CreateMap<City, DataObject>().ReverseMap();
    }
}
