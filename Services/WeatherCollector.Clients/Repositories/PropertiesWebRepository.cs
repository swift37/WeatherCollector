using System.Net.Http.Json;
using WeatherCollector.Domain;

namespace WeatherCollector.Clients.Repositories
{
    public class PropertiesWebRepository : WebRepository<Property>
    {
        public PropertiesWebRepository(HttpClient client) : base(client) { }

        public async Task<IEnumerable<Property>> GetAllMatches(string? name, CancellationToken cancellation = default)
        {
            var response = await _client.GetAsync($"{name}", cancellation).ConfigureAwait(false);

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Property>>(cancellationToken: cancellation);
            return result ?? Enumerable.Empty<Property>();
        }
    }
}
