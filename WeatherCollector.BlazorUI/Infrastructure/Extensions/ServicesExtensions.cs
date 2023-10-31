using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WeatherCollector.BlazorUI.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static IHttpClientBuilder AddAPI<TClient, TImplementation>(this IServiceCollection services, string? address) 
            where TClient : class 
            where TImplementation: class, TClient => 
            services.AddHttpClient<TClient, TImplementation>((host, client) => 
                client.BaseAddress = new(host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress + address));

        public static IHttpClientBuilder AddAPI<TClient>(this IServiceCollection services, string? fullAddress)
            where TClient : class =>
            services.AddHttpClient<TClient>((host, client) =>
                client.BaseAddress = new(fullAddress ?? throw new InvalidDataException("Base URI does not exist.")));
    }
}
