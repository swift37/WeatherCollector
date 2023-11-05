using MetaWeather;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherCollector.BlazorUI;
using WeatherCollector.BlazorUI.Infrastructure.Extensions;
using WeatherCollector.BlazorUI.Services;
using WeatherCollector.BlazorUI.Services.Interfaces;
using WeatherCollector.Clients.Repositories;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAPI<IRepository<City>, WebRepository<City>>("api/citiesrepository/");
builder.Services.AddAPI<IRepository<Source>, WebRepository<Source>>("api/sourcesrepository/");
builder.Services.AddAPI<IRepository<Property>, WebRepository<Property>>("api/propertiesrepository/");
builder.Services.AddAPI<AstroWeatherClient>(builder.Configuration["SourceURI"]);
builder.Services.AddScoped<ICitiesService, CitiesService>();

await builder.Build().RunAsync();
