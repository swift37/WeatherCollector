using AutoMapper;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;

namespace WeatherCollector.API.Infrastructure.Mapping
{
    public class DataSourceMappingProfile : Profile
    {
        public DataSourceMappingProfile() => CreateMap<DataSourceInfo, DataSource>().ReverseMap();
    }
}
