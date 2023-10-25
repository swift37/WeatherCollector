using WeatherCollector.Interfaces.Entities;

namespace WeatherCollector.Interfaces.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<bool> ExistById(int id, CancellationToken cancellation = default);

        Task<bool> Exist(T? entity, CancellationToken cancellation = default);

        Task<int> GetCount(CancellationToken cancellation = default);

        Task<IEnumerable<T>> GetAll(CancellationToken cancellation = default);

        Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancellation = default);

        Task<IPage<T>> GetPage(int index, int size, CancellationToken cancellation = default);

        Task<T?> Get(int id, CancellationToken cancellation = default);

        Task<T?> Add(T? entity, CancellationToken cancellation = default);

        Task<T?> Update(T? entity, CancellationToken cancellation = default);

        Task<T?> Delete(T? entity, CancellationToken cancellation = default);

        Task<T?> DeleteById(int id, CancellationToken cancellation = default);
    }

    public interface IPage<out T>
    {
        IEnumerable<T> Items { get; }

        int Index { get; }

        int Size { get; }

        int TotalItemsCount { get; }

        int TotalPagesCount { get; }
    }
}
