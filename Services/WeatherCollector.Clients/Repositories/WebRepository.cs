using System.Net;
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

        public Task<bool> Exist(T? entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> Create(T? entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> Delete(T? entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> DeleteById(int id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> Get(int id, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPage<T>> GetPage(int index, int size, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> Update(T? entity, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
