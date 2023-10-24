using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Interfaces.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T : INamedEntity
    {
        Task<bool> ExistName(string? name, CancellationToken cancellation = default);

        Task<T> GetByName(string? name, CancellationToken cancellation = default);

        Task<T> DeleteByName(string? name, CancellationToken cancellation = default);
    }
}
