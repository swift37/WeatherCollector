using System.Net;
using System.Net.Http.Json;
using WeatherCollector.DAL.Entities.Base;
using WeatherCollector.Interfaces.Entities;
using WeatherCollector.Interfaces.Repositories;

namespace WeatherCollector.Clients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client) => _client = client;

        public async Task<bool> ExistById(int id, CancellationToken cancellation = default)
        {
            var response = await _client.GetAsync($"exist/{id}", cancellation).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<bool> Exist(T? entity, CancellationToken cancellation = default)
        {
            var response = await _client.PostAsJsonAsync("exist", entity, cancellation).ConfigureAwait(false);
            return response.StatusCode != HttpStatusCode.NotFound && response.IsSuccessStatusCode;
        }

        public async Task<int> GetCount(CancellationToken cancellation = default) => 
            await _client.GetFromJsonAsync<int>("count", cancellation).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellation = default) => 
            await _client.GetFromJsonAsync<IEnumerable<T>>(string.Empty, cancellation).ConfigureAwait(false);

        public async Task<T?> Get(int id, CancellationToken cancellation = default) => 
            await _client.GetFromJsonAsync<T?>($"{id}", cancellation).ConfigureAwait(false);

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancellation = default) =>
            await _client.GetFromJsonAsync<IEnumerable<T>>($"items?skip={skip}&count={count}", cancellation).ConfigureAwait(false);

        public async Task<IPage<T>> GetPage(int index, int size, CancellationToken cancellation = default) =>
            await _client.GetFromJsonAsync<IPage<T>>($"page?index={index}&size={size}", cancellation).ConfigureAwait(false);

        public async Task<T?> Create(T? entity, CancellationToken cancellation = default)
        {
            var response = await _client.PostAsJsonAsync(string.Empty, entity, cancellation).ConfigureAwait(false);
            var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>(cancellationToken: cancellation);
            return result;
        }

        public async Task<T?> Update(T? entity, CancellationToken cancellation = default)
        {
            var response = await _client.PutAsJsonAsync(string.Empty, entity, cancellation).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound) return default;

            var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>(cancellationToken: cancellation);
            return result;
        }

        public async Task<T?> Delete(T? entity, CancellationToken cancellation = default)
        {
            var requestMsg = new HttpRequestMessage(HttpMethod.Delete, string.Empty)
            {
                Content = JsonContent.Create(entity)
            };

            var response = await _client.SendAsync(requestMsg, cancellation).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound) return default;

            var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>(cancellationToken: cancellation);
            return result;
        }

        public async Task<T?> DeleteById(int id, CancellationToken cancellation = default)
        {
            var response = await _client.DeleteAsync($"{id}", cancellation).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound) return default;

            var result = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<T>(cancellationToken: cancellation);
            return result;
        }
    }
}
