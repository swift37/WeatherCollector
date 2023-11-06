using System.Net;
using System.Net.Http.Json;
using WeatherCollector.Interfaces.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.Clients.Repositories
{
    public class WebNamedRepository<T> : WebRepository<T>, INamedRepository<T>
        where T : INamedEntity
    {
        public WebNamedRepository(HttpClient client) : base(client) { }

        public async Task<bool> Exist(string? name, CancellationToken cancellation = default)
        {
            var response = await _client.GetAsync($"exist/{name}", cancellation).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<T?> Get(string? name, CancellationToken cancellation = default) => 
            await _client.GetFromJsonAsync<T?>($"{name}", cancellation).ConfigureAwait(false);

        public async Task<T?> Delete(string? name, CancellationToken cancellation = default)
        {
            var response = await _client.DeleteAsync($"{name}", cancellation).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound) return default;

            var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>(cancellationToken: cancellation);
            return result;
        }
    }
}
