﻿using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Controllers.Base;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.API.Controllers
{
    [Produces("application/json")]
    public class DataSourcesController : EntityController<DataSource>
    {
        public DataSourcesController(IRepository<DataSource> repository) : base(repository) { }
    }
}
