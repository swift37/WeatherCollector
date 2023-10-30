using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherCollector.BlazorUI;
using WeatherCollector.BlazorUI.Infrastructure.Extensions;
using WeatherCollector.Clients.Repositories;
using WeatherCollector.Domain;
using WeatherCollector.Interfaces.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAPI<IRepository<Source>, WebRepository<Source>>("api/sourcesrepository/");

await builder.Build().RunAsync();
