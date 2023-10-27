using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly.Extensions.Http;
using Polly;
using WeatherCollector.Interfaces.Repositories;
using WeatherCollector.DAL.Entities;
using WeatherCollector.Clients.Repositories;

class Program
{
    private static IHost? _Hosting;

    public static IHost Hosting => _Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static IServiceProvider Services => Hosting.Services;

    public static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder()
        .ConfigureServices(ConfigureServices);

    private static void ConfigureServices(HostBuilderContext host, IServiceCollection services) 
    {
        services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(client =>
        {
            client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataSources/");
        });

        services.AddHttpClient<IRepository<DataValue>, WebRepository<DataValue>>(client =>
        {
            client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataValues/");
        });
    }

    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var jitter = new Random();
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(10, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                TimeSpan.FromMilliseconds(jitter.Next(0, 1000)));
    }

    public static async Task Main(string[] args)
    {
        using var host = Hosting;
        await host.StartAsync();

        Console.WriteLine("Completed!");
        Console.ReadLine();

        await host.StopAsync();
    }
}