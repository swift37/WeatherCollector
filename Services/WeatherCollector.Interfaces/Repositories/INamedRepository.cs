using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Interfaces.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T : INamedEntity
    {
        Task<bool> Exist(string? name, CancellationToken cancellation = default);

        Task<T?> Get(string? name, CancellationToken cancellation = default);

        Task<T?> Delete(string? name, CancellationToken cancellation = default);
    }
}
