﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [Produces("application/json")]
    public class SourceRepositoryController : MappedEntityController<Source, DataSource>
    {
        public SourceRepositoryController(IRepository<DataSource> repository, IMapper mapper) 
            : base(repository, mapper) { }
    }
}